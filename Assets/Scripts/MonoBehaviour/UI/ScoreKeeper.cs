using Assets;
using Assets.Scripts;
using Assets.Scripts.Managers;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private bool scoresUpdated = false;

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
        if (GameManager.Instance.GamePaused)
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
       SaveProgress(ReachedTarget ? GameResult.ReachedTarget : GameResult.LimitExpired);
    }

    private void SaveProgress(GameResult result)
    {
        if (scoresUpdated)
        {
            return;
        }

        scoresUpdated = true;
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

}