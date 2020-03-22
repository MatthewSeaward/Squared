using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using NSubstitute;
using System.Linq;
using Assets.Scripts.Workers.Piece_Effects.Piece_Connection;
using static SquarePiece;

[Category("Restrictions")]
public class BannedSprite
{
    Sprite bannedSprite;
    Sprite default1Sprite;
    Sprite default2Sprite;

    Colour bannedColour = Colour.Orange;
    Colour defaultColour1 = Colour.Yellow;
    Colour defaultColour2 = Colour.Red;

    [OneTimeSetUp]  
    public void TestSetup()
    {
        bannedSprite = GetSprite();
        bannedSprite.name = bannedColour.ToString(); ;

        default1Sprite = GetSprite();
        default1Sprite.name = defaultColour1.ToString();

        default2Sprite = GetSprite();
        default2Sprite.name = defaultColour2.ToString();
    }

    [Test]
    public void Normal_ContainsBanned()
    {

        var sut = new Assets.Scripts.Workers.Score_and_Limits.BannedSprite(1);

        var bannedPiece = Substitute.For<ISquarePiece>();
        bannedPiece.Sprite = bannedSprite;

        var normalPiece = Substitute.For<ISquarePiece>();
        normalPiece.Sprite = default1Sprite;

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(normalPiece);
        sequence.AddLast(bannedPiece);
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsTrue(sut.ViolatedRestriction());
    }

    [Test]
    public void Normal_NoBanned()
    {

        var sut = new Assets.Scripts.Workers.Score_and_Limits.BannedSprite(1);

         var normalPiece = Substitute.For<ISquarePiece>();
        normalPiece.Sprite = default1Sprite;

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(normalPiece);
        sequence.AddLast(normalPiece);
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsFalse(sut.ViolatedRestriction());
    }

    [Test]
    public void FadePiece_ContainsBanned()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.BannedSprite(1);

        var bannedPiece = Substitute.For<ISquarePiece>();
        bannedPiece.Sprite = default1Sprite;
        bannedPiece.PieceConnection = new TwoSpriteConnection(bannedPiece, bannedColour);

        var normalPiece = Substitute.For<ISquarePiece>();
        normalPiece.Sprite = default1Sprite;

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(normalPiece);
        sequence.AddLast(bannedPiece);
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsTrue(sut.ViolatedRestriction());
    }

    [Test]
    public void FadePiece_NoBanned()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.BannedSprite(1);

        var piece1 = Substitute.For<ISquarePiece>();
        piece1.Sprite = default1Sprite;
        piece1.PieceConnection = new TwoSpriteConnection(piece1, defaultColour2);

        var piece2 = Substitute.For<ISquarePiece>();
        piece2.Sprite = default1Sprite;

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(piece2);
        sequence.AddLast(piece1);
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsFalse(sut.ViolatedRestriction());
    }


    private Sprite GetSprite()
    {
        return Sprite.Create(new Texture2D(1, 1), new Rect(), new Vector2());
    }
}
