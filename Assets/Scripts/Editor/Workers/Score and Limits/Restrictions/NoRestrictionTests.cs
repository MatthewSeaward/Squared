using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static Assets.Scripts.Workers.Helpers.TestHelpers;

[Category("Restrictions")]
public class NoRestrictionTests
{
	[Test]
	public void True_NoRestriction_3Pieces()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.NoRestriction();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsFalse(sut.ViolatedRestriction());
    }

    [Test]
    public void True_NoRestriction_0Pieces()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.NoRestriction();

        var sequence = new LinkedList<ISquarePiece>();
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsFalse(sut.ViolatedRestriction());
    }
}
