﻿using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.Factorys
{
    abstract class PieceBuilder
    {
        public SquarePiece squarePiece;
        public int scoreValue;
        public bool initialsetup;

        protected abstract (Sprite sprite, Colour colour) GetSprite();
        protected abstract void BuildConnection();
        protected abstract void BuildDestroyEffect();
        protected abstract void BuildBehaviours();
        protected abstract void BuildOnCollection();
        protected abstract void BuildOnDestroy();
        protected abstract void BuildScoring();

        public void BuildPiece()
        {
            BuildSprite();
            BuildConnection();
            BuildDestroyEffect();
            BuildBehaviours();
            BuildOnCollection();
            BuildOnDestroy();
            BuildScoring();

            BuildLayers();
            BuildTextLayer();
        }

        private void BuildSprite()
        {
            var sprite = GetSprite();

            squarePiece.GetComponent<SpriteRenderer>().sprite = sprite.sprite;
            squarePiece.PieceColour = sprite.colour;
        }

        private void BuildTextLayer()
        {
            string displayText = string.Empty;

            ApplyTextLayer(ref displayText, squarePiece.PieceConnection as ITextLayer);
            ApplyTextLayer(ref displayText, squarePiece.PieceBehaviour as ITextLayer);
            ApplyTextLayer(ref displayText, squarePiece.DestroyPieceHandler as ITextLayer);
            ApplyTextLayer(ref displayText, squarePiece.Scoring as ITextLayer);

            squarePiece.SetText(displayText);
        }

        private static void ApplyTextLayer(ref string text, ITextLayer layer)
        {
            if (layer == null)
            {
                return;
            }

            text = layer.GetText();
        }

        private void BuildLayers()
        {
            for (int i = 0; i < squarePiece.transform.childCount; i++)
            {
                var child = squarePiece.transform.GetChild(i);
                if (child == null)
                {
                    continue;
                }
                if (child.gameObject.GetComponent<SpriteRenderer>() == null)
                {
                    continue;
                }

                child.gameObject.SetActive(false);
            }

            ApplyLayer(squarePiece.PieceConnection as ILayeredSprite);
            ApplyLayer(squarePiece.PieceBehaviour as ILayeredSprite);
            ApplyLayer(squarePiece.DestroyPieceHandler as ILayeredSprite);
            ApplyLayer(squarePiece.Scoring as ILayeredSprite);
            ApplyLayer(squarePiece.OnCollection as ILayeredSprite);

        }

        private void ApplyLayer(ILayeredSprite layer)
        {
            if (layer != null && layer.GetSprites() != null)
            {
                foreach (var sprite in layer.GetSprites())
                {
                    var l = ObjectPool.Instantiate(GameResources.GameObjects["PieceLayer"], Vector3.zero);
                    l.GetComponent<SpriteRenderer>().sprite = sprite;
                    l.transform.parent = squarePiece.transform;
                }
            }
        }   

    }
}
