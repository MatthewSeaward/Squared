using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Score_and_Limits;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Daily_Challenge
{
    public static class RandomLevelGenerator
    {
        public static Level_Info.Level GenerateRandomLevel(INumberGenerator numberGenerator)
        {
            var dt = new Level()
            {
                Target = 0,
                Pattern = RandomGridGenerator.GenerateRandomGrid(numberGenerator),
                SpecialDropPieces = GenerateSpecialPieces(numberGenerator),
                colours = GenerateColours(numberGenerator),
            };

            return new Level_Info.Level(dt)
            {
                Star1Progress = GenerateLimit(numberGenerator),
                Star2Progress = null,
                Star3Progress = null
            };
        }

        private static StarProgress GenerateLimit(INumberGenerator numberGenerator)
        {
            var turnLimit = numberGenerator.Range(0, 100) < 80;

            if (turnLimit)
            {
                return new StarProgress()
                {
                    Limit = new TurnLimit(numberGenerator.Range(10, 25)),
                    Restriction = new NoRestriction()
                };
            }
            else
            {
                return new StarProgress()
                {
                    Limit = new TimeLimit(numberGenerator.Range(40, 70)),
                    Restriction = new NoRestriction()
                };
            }
        }

        private static SquarePiece.Colour[] GenerateColours(INumberGenerator numberGenerator)
        {
            var result = new List<SquarePiece.Colour>();

            int numberOfColours = numberGenerator.Range(3, 6);

            var colours = new List<SquarePiece.Colour>();
            colours.AddRange((SquarePiece.Colour[])Enum.GetValues(typeof(SquarePiece.Colour)));
            colours.Remove(SquarePiece.Colour.None);

            for (int i = 0; i < numberOfColours; i++)
            {
                var chosenColour = colours[numberGenerator.Range(0, colours.Count)];
                result.Add(chosenColour);
                colours.Remove(chosenColour);
            }

            return result.ToArray();
        }

        private static string[] GenerateSpecialPieces(INumberGenerator numberGenerator)
        {
            var result = new List<string>();

            int numberOfSpecialPieces = numberGenerator.Range(0, 5);

            var specialPieces = GetSpecialPieces();

            for (int i = 0; i < numberOfSpecialPieces; i++)
            {
                var specialPiece = specialPieces[numberGenerator.Range(0, specialPieces.Count)];
                result.Add(((char)specialPiece).ToString());
                specialPieces.Remove(specialPiece);
            }

            return result.ToArray();
        }
        
        private static List<PieceBuilderDirector.PieceTypes> GetSpecialPieces()
        {
            var specialPieces = new List<PieceBuilderDirector.PieceTypes>();
            specialPieces.AddRange((PieceBuilderDirector.PieceTypes[])Enum.GetValues(typeof(PieceBuilderDirector.PieceTypes)));
            specialPieces.Remove(PieceBuilderDirector.PieceTypes.Normal);
            specialPieces.Remove(PieceBuilderDirector.PieceTypes.Empty);
            specialPieces.Remove(PieceBuilderDirector.PieceTypes.Locked);
            specialPieces.Remove(PieceBuilderDirector.PieceTypes.Chest);
            specialPieces.Remove(PieceBuilderDirector.PieceTypes.Heart);
            return specialPieces;
        }
    }
}
