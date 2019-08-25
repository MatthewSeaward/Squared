using NUnit.Framework;
using Assets.Scripts.Workers.Score_and_Limits;
using System.Collections.Generic;
using static Assets.Scripts.Workers.TestHelpers;

[Category("Restrictions")]
public class MinPieceLimit
{

	[Test]
	public void True_Limit3_Pieces3()
    {
        var sut = new MinSequenceLimit(3);

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sut.SequenceCompleted(sequence);

        Assert.IsFalse(sut.ViolatedRestriction());
    }

    [Test]
    public void True_Limit3_Pieces4()
    {
        var sut = new MinSequenceLimit(3);

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sut.SequenceCompleted(sequence);

        Assert.IsFalse(sut.ViolatedRestriction());
    }

    [Test]
    public void False_Limit3_Pieces2()
    {
        var sut = new MinSequenceLimit(3);

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sut.SequenceCompleted(sequence);

        Assert.IsTrue(sut.ViolatedRestriction());
    }
}
