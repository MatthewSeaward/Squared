﻿using Assets.Scripts.Workers;
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
        private string _queuedTrigger;

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

            PieceSelectionManager.SequenceCompleted += SequenceCompletedEvent;
            SpeechBubbleManager.SpeechBubbleFinishedEvent += DisplayNextText;
            LevelStart.GameStarted += GameStarted;

            ShowEnemyText(DialogueManager.Instance.GetLevelText());
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

            if (!string.IsNullOrEmpty(_queuedTrigger))
            {
                enemy.GetComponent<Animator>().SetTrigger(_queuedTrigger);
                _queuedTrigger = null;
            }
        }

        public void OnDestroy()
        {
            PieceSelectionManager.SequenceCompleted -= SequenceCompletedEvent;
            SpeechBubbleManager.SpeechBubbleFinishedEvent -= DisplayNextText;
            LevelStart.GameStarted -= GameStarted;
        }

        public void ShowEnemyText(params string[] text)
        {
            foreach (var t in text)
            {
                _queuedText.Enqueue(t);
            }
            MoveEnemyOnScreen();
        }

       
        private void MoveEnemyOnScreen()
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

        private void SequenceCompletedEvent(LinkedList<ISquarePiece> pieces)
        {
            if (MenuProvider.Instance.OnDisplay)
            {
                return;
            }

            if (pieces.Count >= enemy.PiecesForRage)
            {
                PlayRage();
                EnemyRaged?.Invoke();
            }
        }

        public void PlayRage()
        {
            _queuedTrigger = "Angry1";

            ShowEnemyText(DialogueManager.Instance.GetAngryText());
            SpeechBubbleManager.SpeechBubbleFinishedEvent += PlayEnemyRage;
        }

        private void PlayEnemyRage()
        {
            SpeechBubbleManager.SpeechBubbleFinishedEvent -= PlayEnemyRage;

            enemy.GetComponent<EnemyScript>().EnemyRage.InvokeRage();
        }
    }
}
