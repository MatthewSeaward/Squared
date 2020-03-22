using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Piece_Connection;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Workers.Helpers.TestHelpers;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Grid_Management.Helpers
{
    public static class BuildGridHelper
    {
        public static void BuildGrid(string[] grid)
        {
            var list = new List<ISquarePiece>();

            Debug.Log("--Grid--");
            for (int y = 0; y < grid.Length; y++)
            {
                Debug.Log(grid[y]);

                for (int x = 0; x < grid[y].Length; x++)
                {
                    var piece = grid[y][x].ToString();
                    if (piece == "r")
                    {
                        list.Add(BuildAnyConnectionPiece(x, y, piece));
                    }
                    else if (piece == "f")
                    {
                        list.Add(BuildFadeConnectionPiece(x, y, piece));
                    }
                    else
                    {
                        list.Add(BuildPiece(x, y, piece));
                    }
                }
            }
            Debug.Log("----");

            PieceManager.Instance.Setup(list, new float[grid[0].Length], new float[grid.Length]);
        }

        private static ISquarePiece BuildPiece(int x, int y, string sprite)
        {
            var piece = CreatePiece();
            piece.Position = new Vector2Int(x, y);
            piece.Sprite = GetSprite(sprite);
            piece.PieceColour = (SquarePiece.Colour)Convert.ToInt32(sprite);
            piece.PieceConnection = new StandardConnection(piece);
            piece.Scoring = new SingleScore();
            return piece;
        }

        private static ISquarePiece BuildAnyConnectionPiece(int x, int y, string sprite)
        {
            var piece = CreatePiece();
            piece.Position = new Vector2Int(x, y);
            piece.Sprite = GetSprite(sprite);
            piece.PieceColour = SquarePiece.Colour.None;
            piece.PieceConnection = new AnyAdjancentConnection(piece);
            piece.Scoring = new SingleScore();
            return piece;
        }

        private static ISquarePiece BuildFadeConnectionPiece(int x, int y, string sprite)
        {
            var piece = CreatePiece();
            piece.Position = new Vector2Int(x, y);
            piece.Sprite = GetSprite("1");
            piece.PieceColour = SquarePiece.Colour.Orange;
            piece.PieceConnection = new TwoSpriteConnection(piece, SquarePiece.Colour.Yellow);
            piece.Scoring = new SingleScore();
            return piece;
        }
    }
}
