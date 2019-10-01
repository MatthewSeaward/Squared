﻿using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.IO.Data_Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ShowScore : MonoBehaviour
    {

        public static ShowScore Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ScoreKeeper.PointsAwarded += ScoreKeeper_PointsAwarded;
        }

        private void OnDestroy()
        {
            ScoreKeeper.PointsAwarded -= ScoreKeeper_PointsAwarded;
        }

        private void ScoreKeeper_PointsAwarded(int points, ISquarePiece[] pieces)
        {
            var position = pieces[pieces.Length-1].transform.position;

            var texture = pieces[0].Sprite.texture;
            var colour = texture.GetTextureColour();

            Show(points, position, colour);
        }

        public void Show(int points, Vector3 spawnPosition, Color colour)
        {
            var position = RectTransformUtility.WorldToScreenPoint(Camera.main, spawnPosition);

            var gameObject = ObjectPool.Instantiate(GameResources.GameObjects["Animated Text"], position);

            gameObject.GetComponent<RectTransform>().SetParent(transform);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.SetSiblingIndex(transform.childCount - 1);          

            gameObject.GetComponent<AnimatedText>().Show($"+{points}", colour);
        }
    }
}
