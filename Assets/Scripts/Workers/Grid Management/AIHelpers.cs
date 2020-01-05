using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Grid_Management
{
    public static class AIHelpers
    {
        public static Stack<ISquarePiece> GetNeighbours(IRestriction restriction, ISquarePiece piece)
        {
            var neighbours = new Stack<ISquarePiece>();

            var x = piece.Position.x;
            var y = piece.Position.y;

            bool DiagonalAllowed = !(restriction is DiagonalRestriction);
            bool StraightAllowed = !(restriction is DiagonalOnlyRestriction);

            if (StraightAllowed)
            {
                AddIfNeighbour(restriction, ref neighbours, piece, x + 1, y);
                AddIfNeighbour(restriction, ref neighbours, piece, x - 1, y);
                AddIfNeighbour(restriction, ref neighbours, piece, x, y + 1);
                AddIfNeighbour(restriction, ref neighbours, piece, x, y - 1);
            }

            if (DiagonalAllowed)
            {
                AddIfNeighbour(restriction, ref neighbours, piece, x + 1, y + 1);
                AddIfNeighbour(restriction, ref neighbours, piece, x - 1, y + 1);
                AddIfNeighbour(restriction, ref neighbours, piece, x + 1, y - 1);
                AddIfNeighbour(restriction, ref neighbours, piece, x - 1, y - 1);
            }

            return neighbours;
        }

        private static void AddIfNeighbour(IRestriction restriction, ref Stack<ISquarePiece> neighbours, ISquarePiece piece, int x, int y)
        {
            if (MoveCheckerHelpers.CheckSpot(piece, x, y))
            {
                var p = PieceManager.Instance.GetPiece(x, y);
                if (restriction is BannedPieceType)
                {
                    if (p.Type == (restriction as BannedPieceType).BannedPiece)
                    {
                        return;
                    }
                }

                if (restriction is BannedSprite)
                {
                    var res = restriction as BannedSprite;
                    if (p.Sprite.name == res.Sprite || p.Sprite.name == res.SpriteValue.ToString())
                    {
                        return;
                    }
                }

                if (restriction is SwapEffectLimit)
                {
                    var res = restriction as SwapEffectLimit;
                    var str1 = p.DestroyPieceHandler.GetType().ToString();

                    if (str1 == res.effect)
                    {
                        return;
                    }
                }

                neighbours.Push(p);
            }
        }
    }
}
