using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using System.Collections.Generic;
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
            ApplyTextLayer(ref displayText, squarePiece.OnDestroy as ITextLayer);

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

            squarePiece.PieceConnection.Layers = ApplyLayer(squarePiece.PieceConnection as ILayeredSprite);
            ApplyLayer(squarePiece.PieceBehaviour as ILayeredSprite);
            ApplyLayer(squarePiece.DestroyPieceHandler as ILayeredSprite);
            ApplyLayer(squarePiece.Scoring as ILayeredSprite);
            ApplyLayer(squarePiece.OnCollection as ILayeredSprite);
            ApplyLayer(squarePiece.OnDestroy as ILayeredSprite);

        }

        private List<GameObject> ApplyLayer(ILayeredSprite layer)
        {
            var createdLayers = new List<GameObject>();

            if (layer == null)
            {
                return createdLayers;
            }

            var layeredSettings = layer.GetLayeredSprites();

            foreach (var sprite in layeredSettings.Sprites)
            {
                var l = ObjectPool.Instantiate(GameResources.GameObjects["PieceLayer"], Vector3.zero);
                l.GetComponent<SpriteRenderer>().sprite = sprite;
                l.GetComponent<SpriteRenderer>().sortingOrder = layeredSettings.OrderInLayer;
                l.transform.parent = squarePiece.transform;
                l.transform.localScale = new Vector3(1, 1, 1);
                l.transform.localPosition = Vector3.zero;

                createdLayers.Add(l);
            }

            return createdLayers;
        }
    }
}
