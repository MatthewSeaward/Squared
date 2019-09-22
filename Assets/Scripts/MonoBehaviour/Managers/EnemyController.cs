﻿using Assets.Scripts.Constants;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Enemy.Events;
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

            var enemies = FindObjectsOfType<EnemyScript>();
            foreach(var enemy in enemies)
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

            MenuProvider.MenuDisplayed += MoveEnemyOffScreen;

            SpeechBubbleManager.SpeechBubbleFinishedEvent += DisplayNextText;
            LevelStart.GameStarted += GameStarted;

            ShowEnemyText(DialogueManager.Instance.GetLevelText());

            EnemyEvents = LevelManager.Instance.SelectedLevel.GetCurrentStar().Events;
            foreach(var e in EnemyEvents.RageEvents)
            {
                e.Start(enemy);
            }
        }

        public void Update()
        {
            EnemyEvents.Update(Time.deltaTime);
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
            SpeechBubbleManager.SpeechBubbleFinishedEvent -= DisplayNextText;
            LevelStart.GameStarted -= GameStarted;
            MenuProvider.MenuDisplayed -= MoveEnemyOffScreen;
        }

        public void ShowEnemyText(params string[] text)
        {
            foreach (var t in text)
            {
                _queuedText.Enqueue(t);
            }
            MoveEnemyOnScreen();
        }
       
        public void MoveEnemyOnScreen()
        {
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
            var lerp = enemy.GetComponent<Lerp>();
            lerp.Setup(_startPos);
        }

        private  void ClearSpeeches()
        {
            _queuedText.Clear();
            SpeechBubbleManager.Instance.Clear();
        }        
    }
}
