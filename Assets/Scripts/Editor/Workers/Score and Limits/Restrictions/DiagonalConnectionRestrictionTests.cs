using NUnit.Framework;
using Assets.Scripts.Workers.Score_and_Limits;
using System.Collections.Generic;
using System.Linq;

[Category("Restrictions")]
public class DiagonalConnectionRestrictionTests
{

    [Test]
    public void TwoPieces_OneDiagonal()
    {
        var sut = new DiagonalRestriction();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece(1, 1));
        sequence.AddLast(CreatePiece(2, 2));
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsTrue(sut.ViolatedRestriction());
    }

    [Test]
    public void FourPieces_OneDiagonal()
    {
        var sut = new DiagonalRestriction();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece(1, 1));
        sequence.AddLast(CreatePiece(1, 2));
        sequence.AddLast(CreatePiece(2, 2));
        sequence.AddLast(CreatePiece(3, 3));

        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsTrue(sut.ViolatedRestriction());
    }

    [Test]
    public void FourPieces_NoDiagonal()
    {
        var sut = new DiagonalRestriction();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece(1, 1));
        sequence.AddLast(CreatePiece(1, 2));
        sequence.AddLast(CreatePiece(2, 2));
        sequence.AddLast(CreatePiece(2, 3));

        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsFalse (sut.ViolatedRestriction());
    }


    private ISquarePiece CreatePiece(int x, int y)
    {
        var piece = Assets.Scripts.Workers.TestHelpers.CreatePiece();
        piece.Position = new UnityEngine.Vector2Int(x, y);
        return piece;
    }
}

