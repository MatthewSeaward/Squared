using NUnit.Framework;
using static Assets.Scripts.Workers.Helpers.TestHelpers;

namespace Assets.Scripts.Editor.Workers.Piece_Effects.Swap_Effects
{
    [Category("Piece Effects")]
    public class LockedSwapTests
    {
        [Test]
        public void SwapPiece_SpriteDoesNotChange()
        {
            var sut = CreatePiece();

            sut.DestroyPieceHandler = new Scripts.Workers.Piece_Effects.Destruction.LockedSwap(sut);
            sut.Sprite = GetSprite("1");

            sut.Destroy();

            Assert.AreEqual("1", sut.Sprite.name);
        }
    }
}
