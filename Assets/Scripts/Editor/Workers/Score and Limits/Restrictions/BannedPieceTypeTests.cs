using NUnit.Framework;
using Assets.Scripts.Workers.Score_and_Limits;
using System.Collections.Generic;
using static PieceFactory;

[Category("Restrictions")]
public class BannedPieceTypeTests
{

    [Test]
    public void BannedRainbow_NotUsed()
    {
        var sut = new BannedPieceType("r");

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece(PieceTypes.Normal));
        sequence.AddLast(CreatePiece(PieceTypes.Normal));
        sequence.AddLast(CreatePiece(PieceTypes.Normal));
        sut.SequenceCompleted(sequence);

        Assert.IsFalse(sut.ViolatedRestriction());
    }


    [Test]
    public void BannedRainbow_Used()
    {
        var sut = new BannedPieceType("r");

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece(PieceTypes.Normal));
        sequence.AddLast(CreatePiece(PieceTypes.Rainbow));
        sequence.AddLast(CreatePiece(PieceTypes.Normal));
        sut.SequenceCompleted(sequence);

        Assert.IsTrue(sut.ViolatedRestriction());
    }

    [Test]
    public void BannedSwapping_NotUsed()
    {
        var sut = new BannedPieceType("s");

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece(PieceTypes.Rainbow));
        sequence.AddLast(CreatePiece(PieceTypes.ThreePoints));
        sequence.AddLast(CreatePiece(PieceTypes.FourPoints));
        sut.SequenceCompleted(sequence);

        Assert.IsFalse(sut.ViolatedRestriction());
    }


    [Test]
    public void BannedSwapping_Used()
    {
        var sut = new BannedPieceType("s");

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece(PieceTypes.Rainbow));
        sequence.AddLast(CreatePiece(PieceTypes.ThreePoints));
        sequence.AddLast(CreatePiece(PieceTypes.Swapping));
        sequence.AddLast(CreatePiece(PieceTypes.FourPoints));
        sut.SequenceCompleted(sequence);

        Assert.IsTrue(sut.ViolatedRestriction());
    }

    private ISquarePiece CreatePiece(PieceTypes type)
    {
        var piece = Assets.Scripts.Workers.TestHelpers.CreatePiece();
        piece.Type = type;
        return piece;
    }
}
