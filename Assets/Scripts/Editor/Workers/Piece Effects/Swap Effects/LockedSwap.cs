using NUnit.Framework;
using static Assets.Scripts.Workers.TestHelpers;

[Category("Piece Effects")]
public class LockedSwap
{
    [Test]
    public void SwapPiece_SpriteDoesNotChange()
    {
        var sut = CreatePiece();

        sut.DestroyPieceHandler = new Assets.Scripts.Workers.Piece_Effects.Destruction.LockedSwap(sut);
        sut.Sprite = GetSprite("1");

        sut.Destroy();

        Assert.AreEqual("1", sut.Sprite.name);
    }

}
