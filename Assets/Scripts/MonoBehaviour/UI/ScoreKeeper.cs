using Assets;
using Assets.Scripts;
using Assets.Scripts.Managers;
using Assets.Scripts.UI;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum GameResult { ReachedTarget, LimitExpired, ViolatedRestriction }

public delegate void PointsAwarded(int points, ISquarePiece[] sequence);
public delegate void GameCompleted(string chapter, int level, int star, int score, GameResult result);

public class ScoreKeeper : MonoBehaviour
{
    public static event PointsAwarded PointsAwarded;
    public static event GameCompleted GameCompleted;
       
    public Text Score;
    public Text Time;
    public Text RestrictionText;

    [SerializeField]
    private ProgressBar LimitProgress;

    [SerializeField]
    private ProgressBar ScoreProgress;

    private bool scoresUpdated = false;

    public IGameLimit GameLimit;
    public IRestriction Restriction;
    private IScoreCalculator ScoreCalculator = new StandardScoreCalculator();

    private int _currentScore = 0;

    private int Target => LevelManager.Instance.SelectedLevel.Target;
    private bool ReachedTarget => _currentScore >= Target;

    private static Color restrictionDisabledColour = new Color(0.7f, 0.7f, 0.7f, 0.7f);

    public void Start()
    {
        PieceSelectionManager.SequenceCompleted += SequenceCompleted;
        var currentLevel = LevelManager.Instance.GetNextLevel();

        GameLimit = currentLevel.GetCurrentLimit();
        GameLimit.Reset();

        Restriction = currentLevel.GetCurrentRestriction();
        Restriction.Reset();

        UpdateScore();
        UpdateLimit();
        UpdateRstriction();
    }

    public void OnDestroy()
    {
        PieceSelectionManager.SequenceCompleted -= SequenceCompleted;
    }

    public void SequenceCompleted(ISquarePiece[] pieces)
    { 
        GameLimit.SequenceCompleted(pieces);
        Restriction.SequenceCompleted(pieces);

        int scoreEarned = ScoreCalculator.CalculateScore(pieces);

        _currentScore += scoreEarned;
        UpdateScore();
        UpdateLimit(UnityEngine.Time.deltaTime);

        if (ReachedTarget)
        {
            SaveProgress(GameResult.ReachedTarget);
        }

        PointsAwarded?.Invoke(scoreEarned, pieces);
    }

    private void UpdateScore()
    {
        Score.text = $"Score: {_currentScore}/{Target}";
        Score.color = ReachedTarget ? Color.green : Color.white;

        ScoreProgress.UpdateProgressBar(_currentScore, Target);
    }

    private void Update()
    {
        UpdateRestriction(UnityEngine.Time.deltaTime);

        if (GameManager.Instance.GamePaused || MenuProvider.Instance.OnDisplay)
        {
            return;
        }

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
        UpdateLimit();
        UpdateRstriction();

        if (GameLimit.ReachedLimit())
        {
            SaveProgress(ReachedTarget ? GameResult.ReachedTarget : GameResult.LimitExpired);
        }
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

    private void UpdateLimit()
    {
        Time.text = GameLimit.GetLimitText();

        LimitProgress.UpdateProgressBar(GameLimit.PercentComplete());
    }

    private void UpdateRstriction()
    {
        RestrictionText.text = Restriction.GetRestrictionText();
        RestrictionText.color = Restriction.Ignored ? restrictionDisabledColour : Color.white;
    }
}
