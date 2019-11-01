using Assets.Scripts.Workers.Data_Managers;
using NUnit.Framework;

[Category("Powerups")]
public class PowerupsEarnedTests
{

    [Test]
    public void Gained10_10Under_0Powerups()
    {
        Assert.AreEqual(0, UserPowerupManager.NumberOfPowerupsEarned(30, 40, 50));
    }

    [Test]
    public void Gained20_10Over_1Powerups()
    {
        Assert.AreEqual(1, UserPowerupManager.NumberOfPowerupsEarned(40, 60, 50));
    }

    [Test]
    public void Gained10_Exact_1Powerups()
    {
        Assert.AreEqual(1, UserPowerupManager.NumberOfPowerupsEarned(40, 50, 50));
    }

    [Test]
    public void Gained100_10Over_2Powerups()
    {
        Assert.AreEqual(2, UserPowerupManager.NumberOfPowerupsEarned(10, 110, 50));
    }

    [Test]
    public void Gained100_2Over_10Powerups()
    {
        Assert.AreEqual(10, UserPowerupManager.NumberOfPowerupsEarned(0, 102, 10));
    }
}
