using NUnit.Framework;
using System.Collections.Generic;

[Category("Limits")]
public class TimeLimitTests
{

    [Test]
    public void TenSecondLimit_OneSecondPast_NotReachedLimit()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.TimeLimit(10);

        sut.Update(1);

        Assert.IsFalse(sut.ReachedLimit());
    }

    [Test]
    public void TenSecondLimit_MultipleSecondsPast_NotReachedLimit()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.TimeLimit(10);

        sut.Update(1);
        Assert.IsFalse(sut.ReachedLimit());

        sut.Update(3);
        Assert.IsFalse(sut.ReachedLimit());
        
        sut.Update(5);
        Assert.IsFalse(sut.ReachedLimit());
    }

    [Test]
    public void TenSecondLimit_TenSecondsPast_ReachedLimit()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.TimeLimit(10);

        sut.Update(10);
        Assert.IsTrue(sut.ReachedLimit());

       
    }

}
