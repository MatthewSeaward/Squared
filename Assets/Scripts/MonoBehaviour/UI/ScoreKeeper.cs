﻿using Assets;
using Assets.Scripts;
using Assets.Scripts.UI.Helpers;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public Text Score;
    public Text Time;

    private IGameLimit GameLimit;
    private IRestriction Restriction; 
    private int _currentScore = 0;
    
    bool ReachedTarget => _currentScore >= LevelManager.Instance.SelectedLevel.Target;

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

        _currentScore += pieces.Count;
        UpdateScore();
    }

    private void UpdateScore()
    {
        Score.text = $"Score: {_currentScore}";
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
            MenuProvider.Instance.ShowPopup("Failed", "You violated the restriction.", 
                new ButtonArgs()
                {
                    Text = "Retry",
                    Action = () => SceneManager.LoadScene(1)
                });
        }
    }

    private void UpdateLimit(float deltaTime)
    {
        GameLimit.Update(UnityEngine.Time.deltaTime);
        
        if (ReachedTarget)
        {
            MenuProvider.Instance.ShowPopup("Victory", $"You scored {_currentScore} out of {LevelManager.Instance.SelectedLevel.Target}!",
                   new ButtonArgs()
                   {
                       Text = "Continue",
                       Enabled = LevelManager.Instance.LevelUnlocked(LevelManager.Instance.CurrentLevel + 1),
                       Action = () => LoadNextLevel()
                   },
                   new ButtonArgs()
                   {
                         Text = "Retry",
                         Action = () => SceneManager.LoadScene(1)
                   }
                   );



            int star =  LevelManager.Instance.SelectedLevel.LevelProgress != null ? LevelManager.Instance.SelectedLevel.LevelProgress.StarAchieved : 0;                    
            LevelManager.Instance.RegisterLevelCompleted(star, _currentScore);
        }
        else if (GameLimit.ReachedLimit())
        {
            MenuProvider.Instance.ShowPopup("Failed", "You did not reach the target score within the limit.", new ButtonArgs()
            {
                Text = "Retry",
                Action = () => SceneManager.LoadScene(1)
            });
        }
        else
        {
            UpdateLimitText();
        }
    }

    private void UpdateLimitText()
    {
        Time.text = GameLimit.GetLimitText() + Environment.NewLine + Restriction.GetRestrictionText();
    }

    public void LoadNextLevel()
    {
        LevelManager.Instance.CurrentLevel++;
        SceneManager.LoadScene(1);
           }
}
