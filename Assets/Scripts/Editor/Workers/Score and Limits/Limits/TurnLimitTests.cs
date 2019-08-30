using NUnit.Framework;
using System.Collections.Generic;

[Category("Limits")]
public class TurnLimitTests
{

    [Test]
    public void ThreeTurnLimit_TakeOneTurn_NotReachedLimit()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.TurnLimit(3);

        sut.SequenceCompleted(new LinkedList<ISquarePiece>());

        Assert.IsFalse (sut.ReachedLimit());
    }

    [Test]
    public void ThreeTurnLimit_TakeThreeTurns_ReachedLimit()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.TurnLimit(3);

        sut.SequenceCompleted(new LinkedList<ISquarePiece>());
        sut.SequenceCompleted(new LinkedList<ISquarePiece>());
        sut.SequenceCompleted(new LinkedList<ISquarePiece>());

        Assert.IsTrue(sut.ReachedLimit());
    }

}
