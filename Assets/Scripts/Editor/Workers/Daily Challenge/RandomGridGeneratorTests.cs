using Assets.Scripts.Workers.Daily_Challenge;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Daily_Challenge
{
    [Category("Daily Challenge")]
    public class RandomGridGeneratorTests
    {
        [Test]
        public void CreateTwentyRandomGrids()
        {
            var randomGenerator = new CustomDayGenerator(26, 04, 2020);

            for (int i = 0; i < 20; i++)
            {
                Debug.Log(i + 1);

                var grid = RandomGridGenerator.GenerateRandomGrid(randomGenerator);

                PrintGrid(grid);
            }           
        }

        [Test]
        public void CreateTwoRandomGrids_SameRandomSeed()
        {
            var randomGenerator1 = new CustomDayGenerator(26, 04, 2020);
            var randomGenerator2 = new CustomDayGenerator(26, 04, 2020);

            var grid1 = RandomGridGenerator.GenerateRandomGrid(randomGenerator1);
            var grid2 = RandomGridGenerator.GenerateRandomGrid(randomGenerator2);

            AssertGridsEqual(grid1, grid2);
        }

        [Test]
        public void CreateTwoRandomGrids_DifferenteRandomSeed()
        {
            var randomGenerator1 = new CustomDayGenerator(26, 04, 2020);
            var randomGenerator2 = new CustomDayGenerator(27, 04, 2020);


            var grid1 = RandomGridGenerator.GenerateRandomGrid(randomGenerator1);
            var grid2 = RandomGridGenerator.GenerateRandomGrid(randomGenerator1);

            AssertGridsDifferent(grid1, grid2);
        }

        private void AssertGridsEqual(string[] grid1, string[] grid2)
        {
            for (int i = 0; i < grid1.Length; i++)
            {
                if (grid1[i] != grid2[i])
                {
                    PrintGrid(grid1);
                    PrintGrid(grid2);

                    throw new AssertionException($"Grids do not match. Difference between {grid1[i]} and {grid2[2]}");
                }
            }
        }

        private void AssertGridsDifferent(string[] grid1, string[] grid2)
        {
            for (int i = 0; i < grid1.Length; i++)
            {
                if (grid1[i] != grid2[i])
                {
                    return;                   
                }
            }

            PrintGrid(grid1);
            PrintGrid(grid2);

            throw new AssertionException($"Grids do match.");
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
