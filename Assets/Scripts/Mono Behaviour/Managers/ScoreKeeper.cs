using Assets;
using Assets.Scripts;
using Assets.Scripts.Workers.Powerups;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System;
using UnityEngine;

[Serializable]
public enum GameResult { ReachedTarget, LimitExpired, ViolatedRestriction }

public delegate void PointsAwarded(int points, ISquarePiece[] sequence);
public delegate void GameCompleted(string chapter, int level, int star, int score, GameResult result);
public delegate void CurrentPointsChanged(int newPoints, int target);
public delegate void BonusChanged(string currentBonus);

public class ScoreKeeper : MonoBehaviour
{
    public static event PointsAwarded PointsAwarded;
    public static event GameCompleted GameCompleted;
    public static event CurrentPointsChanged CurrentPointsChanged;
    public static event BonusChanged BonusChanged;
    
    private bool scoresUpdated = false;

    public IGameLimit GameLimit;
    private IScoreCalculator ScoreCalculator = new StandardScoreCalculator();

    private int _currentScore = 0;

    private int Target => LevelManager.Instance.SelectedLevel.Target;
    private bool ReachedTarget => CurrentScore >= Target;

    private int CurrentScore
    {
        get
        {
            return _currentScore;
        }
        set
        {
            _currentScore = value;
            CurrentPointsChanged?.Invoke(_currentScore, Target);
        }
    }

    public void Start()
    {
        PieceSelectionManager.SequenceCompleted += SequenceCompleted;
        ExtraPoints.BonusAdded += ExtraPoints_BonusAdded;
        RestrictionDisplay.RestrictionViolated += RestrictionViolated;
        LimitDisplay.LimitReached += LimitDisplay_LimitReached;               

        CurrentScore = 0;
        BonusChanged?.Invoke(ScoreCalculator.ActiveBonus);
    }

    public void OnDestroy()
    {
        PieceSelectionManager.SequenceCompleted -= SequenceCompleted;
        ExtraPoints.BonusAdded -= ExtraPoints_BonusAdded;
        RestrictionDisplay.RestrictionViolated -= RestrictionViolated;
        LimitDisplay.LimitReached -= LimitDisplay_LimitReached;
    }

    public void SequenceCompleted(ISquarePiece[] pieces)
    { 
        int scoreEarned = ScoreCalculator.CalculateScore(pieces);

        GainPoints(scoreEarned, pieces);
    }

    public void GainPoints(int scoreEarned, ISquarePiece[] pieces)
    {
        PointsAwarded?.Invoke(scoreEarned, pieces);

        CurrentScore += scoreEarned;

        BonusChanged?.Invoke(ScoreCalculator.ActiveBonus);

        if (ReachedTarget)
        {
            SaveProgress(GameResult.ReachedTarget);
        }
    }

    private void ExtraPoints_BonusAdded(ScoreBonus bonus)
    {
        BonusChanged?.Invoke(ScoreCalculator.ActiveBonus);
    }

    private void LimitDisplay_LimitReached()
    {
        SaveProgress(ReachedTarget ? GameResult.ReachedTarget : GameResult.LimitExpired);
    }

    private void RestrictionViolated()
    {
        SaveProgress(GameResult.ViolatedRestriction);
    }

    private void SaveProgress(GameResult result)
    {
        if (scoresUpdated)
        {
            return;
        }

        scoresUpdated = true;
        GameCompleted?.Invoke(LevelManager.Instance.SelectedChapter, LevelManager.Instance.CurrentLevel, LevelManager.Instance.SelectedLevel.GetCurrentStar().Number, CurrentScore, result);
    }
}
