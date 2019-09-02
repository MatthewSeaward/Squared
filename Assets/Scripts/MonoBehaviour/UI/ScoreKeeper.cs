using Assets;
using Assets.Scripts;
using Assets.Scripts.UI.Helpers;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static Assets.Scripts.Constants.Scenes;

[Serializable]
public enum GameResult { ReachedTarget, LimitExpired, ViolatedRestriction }

public delegate void PointsAwarded(int points, LinkedList<ISquarePiece> pieces);
public delegate void GameCompleted(string chapter, int level, int star, int score, GameResult result);

public class ScoreKeeper : MonoBehaviour
{
    public static event PointsAwarded PointsAwarded;
    public static event GameCompleted GameCompleted;
       
    public Text Score;
    public Text Time;

    private IGameLimit GameLimit;
    private IRestriction Restriction;
    private IScoreCalculator ScoreCalculator = new StandardScoreCalculator();

    private int _currentScore = 0;

    private int Target => LevelManager.Instance.SelectedLevel.Target;
    private bool ReachedTarget => _currentScore >= Target;

    public void Start()
    {
        PieceSelectionManager.SequenceCompleted += SequenceCompleted;
        var currentLevel = LevelManager.Instance.GetNextLevel();

        GameLimit = currentLevel.GetCurrentLimit();
        GameLimit.Reset();

        Restriction = currentLevel.GetCurrentRestriction();
        Restriction.Reset();

        UpdateScore();
        UpdateLimitText();
    }

    public void OnDestroy()
    {
        PieceSelectionManager.SequenceCompleted -= SequenceCompleted;
    }

    public void SequenceCompleted(LinkedList<ISquarePiece> pieces)
    { 
        GameLimit.SequenceCompleted(pieces);
        Restriction.SequenceCompleted(pieces);

        int scoreEarned = ScoreCalculator.CalculateScore(pieces);

        _currentScore += scoreEarned;
        UpdateScore();

        PointsAwarded?.Invoke(scoreEarned, pieces);
    }

    private void UpdateScore()
    {
        Score.text = $"Score: {_currentScore}/{Target}";
        Score.color = ReachedTarget ? Color.green : Color.white;
    }

    private void Update()
    {
        if (MenuProvider.Instance.OnDisplay)
        {
            return;
        }

        UpdateRestriction(UnityEngine.Time.deltaTime);
        UpdateLimit(UnityEngine.Time.deltaTime);
    }

    private void UpdateRestriction(float deltaTime)
    {
        Restriction.Update(deltaTime);

        if (Restriction.ViolatedRestriction())
        {
            SaveProgress(GameResult.ViolatedRestriction);

            MenuProvider.Instance.ShowPopup("Failed", "You violated the restriction.", 
                new ButtonArgs()
                {
                    Text = "Retry",
                    Action = () => SceneManager.LoadScene(Game)
                });
        }
    }

    private void UpdateLimit(float deltaTime)
    {
        GameLimit.Update(UnityEngine.Time.deltaTime);
        UpdateLimitText();

        if (GameLimit.ReachedLimit())
        {
            ProcessGameLimitReached();
        }
    }

    private void ProcessGameLimitReached()
    {
        if (ReachedTarget)
        {
            SaveProgress(GameResult.ReachedTarget);

            int star = LevelManager.Instance.SelectedLevel.LevelProgress != null ? LevelManager.Instance.SelectedLevel.LevelProgress.StarAchieved : 0;
            LevelManager.Instance.RegisterLevelCompleted(star, _currentScore);

            MenuProvider.Instance.ShowPopup("Victory", $"You scored {_currentScore} out of {Target}!",
                   new ButtonArgs()
                   {
                       Text = "Continue",
                       Enabled = LevelManager.Instance.LevelUnlocked(LevelManager.Instance.CurrentLevel + 1),
                       Action = () => LoadNextLevel()
                   },
                   new ButtonArgs()
                   {
                       Text = "Retry",
                       Action = () => SceneManager.LoadScene(Game)
                   }
          );
        }
        else
        {
            SaveProgress(GameResult.LimitExpired);

            MenuProvider.Instance.ShowPopup("Failed", "You did not reach the target score within the limit.", new ButtonArgs()
            {
                Text = "Retry",
                Action = () => SceneManager.LoadScene(Game)
            });
        }
    }

    private void SaveProgress(GameResult result)
    {
        GameCompleted?.Invoke(LevelManager.Instance.SelectedChapter, LevelManager.Instance.CurrentLevel, LevelManager.Instance.SelectedLevel.GetCurrentStar().Number, _currentScore, result);
    }

    private void UpdateLimitText()
    {
        string restrictionText = Restriction.GetRestrictionText();
        if (!string.IsNullOrWhiteSpace(restrictionText))
        {
            restrictionText = Environment.NewLine + restrictionText;
        }

        Time.text = GameLimit.GetLimitText() + restrictionText;
    }

    public void LoadNextLevel()
    {
        LevelManager.Instance.CurrentLevel++;
        SceneManager.LoadScene(Game);
    }
}