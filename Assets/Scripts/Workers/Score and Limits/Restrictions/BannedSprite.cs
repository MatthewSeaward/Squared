using Assets.Scripts.Workers.Generic_Interfaces;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Piece_Connection;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class BannedSprite : IRestriction, IHasSprite
    {
        public readonly string Sprite;
        private bool failed = false;
        public bool Ignored { get; private set; }

        public int SpriteValue { get; private set; }

        public BannedSprite(int spriteValue)
        {
            Sprite = ((SquarePiece.Colour) spriteValue).ToString();
            SpriteValue = spriteValue;
        }

        public string GetRestrictionText()
        {
            return "Banned: ";
        }

        public string GetRestrictionDescription() => GetRestrictionText();

        public bool ViolatedRestriction()
        {
            return failed;
        }

        public void Reset()
        {
            failed = false;
        }

        public void SequenceCompleted(ISquarePiece[] sequence)
        {
            failed = IsRestrictionViolated(sequence);
            Ignored = false;
        }

        public bool IsRestrictionViolated(ISquarePiece[] sequence)
        {
            if (Ignored)
            {
                return false;
            }

            foreach (var item in sequence)
            {
                if (item.Sprite.name == Sprite || item.Sprite.name == SpriteValue.ToString())
                {
                    return true;
                }

                if (item.PieceConnection is TwoSpriteConnection)
                {
                    var fadePiece = item.PieceConnection as TwoSpriteConnection;
                    if (fadePiece.BannedPiece == (Colour) SpriteValue)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public void Ignore()
        {
            Ignored = true;
        }

        public Sprite GetSprite()
        {
            return GameResources.PieceSprites[SpriteValue.ToString()];
        }
    }
}
