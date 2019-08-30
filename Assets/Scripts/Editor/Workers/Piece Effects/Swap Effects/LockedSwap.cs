using NUnit.Framework;
using static Assets.Scripts.Workers.TestHelpers;

[Category("Piece Effects")]
public class LockedSwap
{
    [Test]
    public void SwapPiece_SpriteDoesNotChange()
    {  
         var sut = CreatePiece();

        sut.SwapEffect = new Assets.Scripts.Workers.Piece_Effects.SwapEffects.LockedSwap();
        sut.Sprite = GetSprite("1");

        sut.SwapEffect.ProcessSwap(sut);

        Assert.AreEqual("1", sut.Sprite.name);
    }

}
