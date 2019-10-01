using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using NSubstitute;
using System.Linq;

[Category("Restrictions")]
public class BannedSprite
{
    Sprite bannedSprite;
    Sprite defaultSprite;

    [OneTimeSetUp]  
    public void TestSetup()
    {
        bannedSprite = GetSprite();
        bannedSprite.name = "1";

        defaultSprite = GetSprite();
        defaultSprite.name = "2";
    }

    [Test]
    public void True_ContainsBanned()
    {

        var sut = new Assets.Scripts.Workers.Score_and_Limits.BannedSprite(1);

        var bannedPiece = Substitute.For<ISquarePiece>();
        bannedPiece.Sprite = bannedSprite;

        var normalPiece = Substitute.For<ISquarePiece>();
        normalPiece.Sprite = defaultSprite;

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(normalPiece);
        sequence.AddLast(bannedPiece);
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsTrue(sut.ViolatedRestriction());
    }

    [Test]
    public void False_NoBanned()
    {

        var sut = new Assets.Scripts.Workers.Score_and_Limits.BannedSprite(1);

         var normalPiece = Substitute.For<ISquarePiece>();
        normalPiece.Sprite = defaultSprite;

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(normalPiece);
        sequence.AddLast(normalPiece);
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsFalse(sut.ViolatedRestriction());
    }

    private Sprite GetSprite()
    {
        return Sprite.Create(new Texture2D(1, 1), new Rect(), new Vector2());
    }
}
