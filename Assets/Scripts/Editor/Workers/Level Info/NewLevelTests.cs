using Assets.Scripts.Workers.Level_Info;
using NUnit.Framework;

namespace Assets.Scripts.Editor.Workers.Level_Info
{
    [Category("Level Info")]
    class NewLevelTests
    {
        [Test]
        public void DefaultConstructor()
        {
            Assert.IsNotNull(new Level());
        }

        [Test]
        public void Empty_DataEntities()
        {
            Assert.IsNotNull(new Level(new Scripts.Workers.IO.Data_Entities.Level()));
        }

        [Test]
        public void Complete_DataEntities()
        {
            var colours = new SquarePiece.Colour[] { SquarePiece.Colour.Red, SquarePiece.Colour.Green };
            var pattern = new string[] { "xxxxxxx" };
            var dropPieces = new string[] { "r", "s" };

            var dto = new Scripts.Workers.IO.Data_Entities.Level()
            {
                LevelNumber = 1,
                Target = 100,
                colours = colours,
                Pattern = pattern,
                SpecialDropPieces = dropPieces,
                StarsToUnlock = 5
            };

            var sut = new Level(dto);

            Assert.AreEqual(1, sut.LevelNumber);
            Assert.AreEqual(100, sut.Target);
            CollectionAssert.AreEquivalent(colours, sut.colours);
            CollectionAssert.AreEquivalent(pattern, sut.Pattern);
            CollectionAssert.AreEquivalent(dropPieces, sut.SpecialDropPieces);
            Assert.AreEqual(5, sut.StarsToUnlock);
        }
    }
}