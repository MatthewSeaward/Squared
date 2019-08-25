using Assets.Scripts.Workers.Piece_Effects;
using NUnit.Framework;

[Category("Scoring")]
public class SingleScoreTests
{

    [Test]
    public void Equals_Start0_Score1()
    {
        var scoring = new SingleScore();

        int currentScore = 0;
        Assert.AreEqual(1, scoring.ScorePiece(currentScore));
    }

    [Test]
    public void Equals_Start5_Score1()
    {
        var scoring = new SingleScore();

        int currentScore = 5;
        Assert.AreEqual(6, scoring.ScorePiece(currentScore));
    }
}
