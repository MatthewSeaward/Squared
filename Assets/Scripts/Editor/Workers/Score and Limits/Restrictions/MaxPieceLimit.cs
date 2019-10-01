using NUnit.Framework;
using Assets.Scripts.Workers.Score_and_Limits;
using System.Collections.Generic;
using static Assets.Scripts.Workers.TestHelpers;
using System.Linq;

[Category("Restrictions")]
public class MaxPieceLimit
{

	[Test]
	public void True_MaxLimit3_Pieces2()
    {
        var sut = new MaxSequenceLimit(3);

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsFalse(sut.ViolatedRestriction());
    }

    [Test]
    public void True_MaxLimit3_Pieces3()
    {
        var sut = new MaxSequenceLimit(3);

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsFalse(sut.ViolatedRestriction());
    }

    [Test]
    public void False_MaxLimit3_Pieces4()
    {
        var sut = new MaxSequenceLimit(3);

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());

        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsTrue(sut.ViolatedRestriction());
    }
}
