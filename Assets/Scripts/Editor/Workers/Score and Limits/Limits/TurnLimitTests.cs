using NUnit.Framework;
using System.Collections.Generic;

[Category("Limits")]
public class TurnLimitTests
{

    [Test]
    public void ThreeTurnLimit_TakeOneTurn_NotReachedLimit()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.TurnLimit(3);

        sut.SequenceCompleted(new ISquarePiece[0]);

        Assert.IsFalse (sut.ReachedLimit());
    }

    [Test]
    public void ThreeTurnLimit_TakeThreeTurns_ReachedLimit()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.TurnLimit(3);

        sut.SequenceCompleted(new ISquarePiece[0]);
        sut.SequenceCompleted(new ISquarePiece[0]);
        sut.SequenceCompleted(new ISquarePiece[0]);

        Assert.IsTrue(sut.ReachedLimit());
    }

}
