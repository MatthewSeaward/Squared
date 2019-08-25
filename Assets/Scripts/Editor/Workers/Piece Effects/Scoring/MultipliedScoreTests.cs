using Assets.Scripts.Workers.Piece_Effects;
using NUnit.Framework;

[Category("Scoring")]
public class MultipliedScoreTests
{

    [Test]
    public void Equals_Start0_Multipliedby2()
    {
        var scoring = new MultipliedScore(2);

        int currentScore = 0;
        Assert.AreEqual(0, scoring.ScorePiece(currentScore));
    }

    [Test]
    public void Equals_Start1_Multipliedby2()
    {
        var scoring = new MultipliedScore(2);

        int currentScore = 1;
        Assert.AreEqual(2, scoring.ScorePiece(currentScore));
    }

    [Test]
    public void Equals_Start7_Multipliedby2()
    {
        var scoring = new MultipliedScore(2);

        int currentScore = 7;
        Assert.AreEqual(14, scoring.ScorePiece(currentScore));
    }


    [Test]
    public void Equals_Start7_Multipliedby3()
    {
        var scoring = new MultipliedScore(3);

        int currentScore = 7;
        Assert.AreEqual(21, scoring.ScorePiece(currentScore));
    }

    [Test]
    public void Equals_Start2_Multipliedby3_And3()
    {
        var scoring = new MultipliedScore(3);

        int currentScore = 2;
        var newScore = scoring.ScorePiece(currentScore);
        newScore = scoring.ScorePiece(newScore);
        Assert.AreEqual(18, newScore);
    }
}
