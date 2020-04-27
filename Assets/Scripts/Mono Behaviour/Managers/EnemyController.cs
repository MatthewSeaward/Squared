﻿using Assets.Scripts.Managers;
using Assets.Scripts.Workers.Enemy.Events;
using Assets.Scripts.Workers.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using VikingCrew.Tools.UI;

namespace Assets.Scripts
{
    public delegate void EnemyRaged();

    public class EnemyController : MonoBehaviour
    {
        public static EnemyRaged EnemyRaged;

        private EnemyScript enemy;
        private GameObject enemyHead;

        public static EnemyController Instance { get; private set; }

        private Queue<string> _queuedText = new Queue<string>();
        private Vector3 _targetPos;
        private Vector3 _startPos;

        private EnemyEvents EnemyEvents = new EnemyEvents();

        public void Start()
        {
            Instance = this;

            AddEvents();

            ConfigureEnemy();

            ShowEnemyText(DialogueManager.Instance.GetLevelText());
        }

        private void SetupEnemyEvents()
        {
            EnemyEvents = LevelManager.Instance.SelectedLevel.GetCurrentStar().Events;
            if (EnemyEvents == null)
            {
                return;
            }

            foreach (var e in EnemyEvents.RageEvents)
            {
                e.Start(enemy);
            }
        }

        private void ConfigureEnemy()
        {
            var enemies = FindObjectsOfType<EnemyScript>();
            foreach (var enemy in enemies)
            {
                if (enemy.name != LevelManager.Instance.SelectedChapter)
                {
                    enemy.gameObject.SetActive(false);
                    continue;
                }
                enemy.gameObject.SetActive(true);
                this.enemy = enemy;
                this.enemyHead = enemy.EnemyHead;
            }

            _targetPos = enemy.transform.position;
            _startPos = new Vector3(_targetPos.x - 6, _targetPos.y);
            enemy.transform.position = _startPos;
            enemy.gameObject.AddComponent<Lerp>();

            SetupEnemyEvents();
        }

        private void AddEvents()
        {
            MenuProvider.MenuDisplayed += MenuProvider_MenuDisplayed;
            ScoreKeeper.GameCompleted += ScoreKeeper_GameCompleted;
            SpeechBubbleManager.SpeechBubbleFinishedEvent += DisplayNextText;
            LevelStart.GameStarted += GameStarted;
        }

        public void Update()
        {
            EnemyEvents?.Update(Time.deltaTime);
        }

        private void DisplayNextText()
        {
            if (_queuedText.Count == 0)
            {
                MoveEnemyOffScreen();
                return;
            }

            var nextMessage = _queuedText.Dequeue();

            var time = nextMessage.Length > 10 ? 4f : 2f;

            SpeechBubbleManager.Instance.AddSpeechBubble(enemyHead.transform, nextMessage, timeToLive: time);
        }

        public void OnDestroy()
        {
            if (EnemyEvents != null)
            {
                foreach (var e in EnemyEvents?.RageEvents)
                {
                    e.Dispose();
                }
            }

            SpeechBubbleManager.SpeechBubbleFinishedEvent -= DisplayNextText;
            LevelStart.GameStarted -= GameStarted;
            MenuProvider.MenuDisplayed -= MenuProvider_MenuDisplayed;
            ScoreKeeper.GameCompleted -= ScoreKeeper_GameCompleted;
        }

        private void MenuProvider_MenuDisplayed(Type type)
        {
            if (type == typeof(LevelStart))
            {
                return;
            }

            MoveEnemyOffScreen();
        }

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result)
        {
            if (result == GameResult.ReachedTarget)
            {
                ShowEnemyText_Internal(DialogueManager.Instance.GetPlayerVictoryText());
                enemy.GetComponent<Animator>().SetTrigger("Angry1");

            }
            else
            {
                ShowEnemyText_Internal(DialogueManager.Instance.GetPlayerDefeatText());
            }
        }

        public void ShowEnemyText(params string[] text)
        {
            if (GameManager.Instance.GameOver)
            {
                return;
            }

            if (LevelManager.Instance.DailyChallenge)
            {
                return;
            }

            ShowEnemyText_Internal(text);
        }

        private void ShowEnemyText_Internal(params string[] text)
        {
            if (LevelManager.Instance.DailyChallenge)
            {
                return;
            }

            foreach (var t in text)
            {
                _queuedText.Enqueue(t);
            }
            MoveEnemyOnScreen();
        }        
       
        public void MoveEnemyOnScreen()
        {
            GameManager.Instance.ChangePauseState(this, true);

            var lerp = enemy.GetComponent<Lerp>();
            lerp.LerpCompleted += DisplayNextText;
            lerp.Setup(_targetPos);
        }

        private void GameStarted()
        {
            MoveEnemyOffScreen();
            ClearSpeeches();
        }

        private void MoveEnemyOffScreen()
        {
            if (enemy.transform.position == _startPos)
            {
                return;
            }

            var lerp = enemy.GetComponent<Lerp>();
            lerp.LerpCompleted += MoveOffScreenCompleted;
            lerp.Setup(_startPos);

        }

        private void MoveOffScreenCompleted()
        {
            var lerp = enemy.GetComponent<Lerp>();
            lerp.LerpCompleted -= MoveOffScreenCompleted;

            GameManager.Instance.ChangePauseState(this, false);
        }

        private void ClearSpeeches()
        {
            _queuedText.Clear();
            SpeechBubbleManager.Instance.Clear();
        }        
    }
}
