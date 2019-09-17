using Assets.Scripts.Workers.Helpers.Extensions;
using NUnit.Framework;

[Category("Extensions")]
public class SpacedTests
{

    [Test]
    public void SimpleWord_TwoCapital()
    {
        var sut = "HelloWorld";

        Assert.AreEqual("Hello World", sut.Spaced());
    }

    [Test]
    public void SimpleWord_FourCapital()
    {
        var sut = "MyNameIsBob";

        Assert.AreEqual("My Name Is Bob", sut.Spaced());
    }

    [Test]
    public void SimpleWord_OneCapital()
    {
        var sut = "Helloworld";

        Assert.AreEqual("Helloworld", sut.Spaced());
    }

    [Test]
    public void SimpleWord_NoCapital()
    {
        var sut = "helloworld";

        Assert.AreEqual("helloworld", sut.Spaced());
    }

    [Test]
    public void AllCaps()
    {
        var sut = "HELLO";

        Assert.AreEqual("HELLO", sut.Spaced());
    }

    [Test]
    public void WordWithNumbers()
    {
        var sut = "EasyAs123";

        Assert.AreEqual("Easy As 123", sut.Spaced());
    }
}
