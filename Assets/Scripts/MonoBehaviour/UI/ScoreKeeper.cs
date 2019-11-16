using Assets;
using Assets.Scripts;
using Assets.Scripts.Managers;
using Assets.Scripts.UI;
using Assets.Scripts.Workers.Powerups;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using Assets.Scripts.Workers.Score_and_Limits.ScoreCalculation;
using System;
using UnityEngine;
using UnityEngine.UI;

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

    public Text Time;
    public Text RestrictionText;

    [SerializeField]
    private ProgressBar LimitProgress;

    private bool scoresUpdated = false;

    public IGameLimit GameLimit;
    public IRestriction Restriction;
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

    private static Color restrictionDisabledColour = new Color(0.7f, 0.7f, 0.7f, 0.7f);

    public void Start()
    {
        PieceSelectionManager.SequenceCompleted += SequenceCompleted;
        ExtraPoints.BonusAdded += ExtraPoints_BonusAdded;

        var currentLevel = LevelManager.Instance.GetNextLevel();

        GameLimit = currentLevel.GetCurrentLimit();
        GameLimit.Reset();

        Restriction = currentLevel.GetCurrentRestriction();
        Restriction.Reset();

        UpdateLimit();
        UpdateRstriction();

        CurrentScore = 0;
        BonusChanged?.Invoke(ScoreCalculator.ActiveBonus);
    }

    public void OnDestroy()
    {
        PieceSelectionManager.SequenceCompleted -= SequenceCompleted;
        ExtraPoints.BonusAdded -= ExtraPoints_BonusAdded;
    }
    
    public void SequenceCompleted(ISquarePiece[] pieces)
    { 
        Restriction.SequenceCompleted(pieces);

        int scoreEarned = ScoreCalculator.CalculateScore(pieces);
        PointsAwarded?.Invoke(scoreEarned, pieces);

        CurrentScore += scoreEarned;


        UpdateLimit(UnityEngine.Time.deltaTime);
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
        GameCompleted?.Invoke(LevelManager.Instance.SelectedChapter, LevelManager.Instance.CurrentLevel, LevelManager.Instance.SelectedLevel.GetCurrentStar().Number, CurrentScore, result);
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

    internal void ActivateLimit()
    {
        Time.GetComponent<Animator>().SetTrigger("Activate");
    }

    internal void ActivateRestriction()
    {
        RestrictionText.GetComponent<Animator>().SetTrigger("Activate");
    }
}
