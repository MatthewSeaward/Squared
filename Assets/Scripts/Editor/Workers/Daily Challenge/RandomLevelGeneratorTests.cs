using Assets.Scripts.Workers.Daily_Challenge;
using Assets.Scripts.Workers.Level_Info;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Daily_Challenge
{
    [Category("Daily Challenge")]
    public class RandomLevelGeneratorTests
    {
        [Test]
        public void CreateTwentyRandomLevels()
        {
            var randomGenerator = new CustomDayGenerator(26, 04, 2020);

            for (int i = 0; i < 20; i++)
            {
                Debug.Log(i + 1);

                var level = RandomLevelGenerator.GenerateRandomLevel(randomGenerator);

                PrintLevel(level);
            }
        }

        [Test]
        public void CreateTwoRandomLevels_SameSeed_TimeDelay()
        {
            var level1 = RandomLevelGenerator.GenerateRandomLevel(new CurrentDayGenerator());

            Thread.Sleep(60000);

            var level2 = RandomLevelGenerator.GenerateRandomLevel(new CurrentDayGenerator());

            AssertLevelsEqual(level1, level2);
        }

        [Test]
        public void CreateTwoRandomLevels_SameRandomSeed()
        {
            var randomGenerator1 = new CurrentDayGenerator();
            var randomGenerator2 = new CurrentDayGenerator();

            var level1 = RandomLevelGenerator.GenerateRandomLevel(randomGenerator1);
            var level2 = RandomLevelGenerator.GenerateRandomLevel(randomGenerator2);

            AssertLevelsEqual(level1, level2);
        }

        [Test]
        public void CreateTwoRandomLevels_DifferenteRandomSeed()
        {
            var randomGenerator1 = new CustomDayGenerator(26, 04, 2020);
            var randomGenerator2 = new CustomDayGenerator(27, 04, 2020);

            var level1 = RandomLevelGenerator.GenerateRandomLevel(randomGenerator1);
            var level2 = RandomLevelGenerator.GenerateRandomLevel(randomGenerator1);

            AssertLevelsDifferent(level1, level2);
        }

        private void AssertLevelsEqual(Level level1, Level level2)
        {
            if (!level1.Colours.SequenceEqual(level2.Colours)) throw new AssertionException($"Levels do not match");
            if (!level1.SpecialDropPieces.SequenceEqual(level2.SpecialDropPieces)) throw new AssertionException($"Levels do not match"); 
            if (level1.Star1Progress.Limit.GetDescription() != level2.Star1Progress.Limit.GetDescription()) throw new AssertionException($"Levels do not match"); ;

            for (int i = 0; i < level1.Pattern.Length; i++)
            {
                if (level1.Pattern[i] != level1.Pattern[i])
                {
                    PrintGrid(level1.Pattern);
                    PrintGrid(level2.Pattern);

                    throw new AssertionException($"Grids do not match. Difference between {level1.Pattern[i]} and {level2.Pattern[2]}");
                }
            }
        }

        private void AssertLevelsDifferent(Level level1, Level level2)
        {           
            if (level1.Colours.SequenceEqual(level2.Colours)) return;
            if (level1.SpecialDropPieces.SequenceEqual(level2.SpecialDropPieces)) return;
            if (level1.Star1Progress.Limit.GetDescription() != level2.Star1Progress.Limit.GetDescription()) return;

            for (int i = 0; i < level1.Pattern.Length; i++)
            {
                if (level1.Pattern[i] != level2.Pattern[i])
                {
                    return;
                }
            }

            PrintGrid(level1.Pattern);
            PrintGrid(level2.Pattern);

            throw new AssertionException($"Levels do match.");
        }



        private void PrintLevel(Level level)
        {
            Debug.Log(string.Join(",", level.Colours));
            Debug.Log(string.Join(",", level.SpecialDropPieces));
            Debug.Log(level.Star1Progress.Limit.GetDescription());
            Debug.Log("");
            PrintGrid(level.Pattern);

            Debug.Log("");
        }

        private void PrintGrid(string[] grid)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                Debug.Log(grid[i]);
            }
            Debug.Log("");

        }
    }
}
