using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.ScoreCalculation;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static Assets.Scripts.Workers.TestHelpers;

[Category("Score Calculation")]
public class StandardScoreCalculationWithBonusMultiplier
{
    [OneTimeSetUp]
    public void TestStart()
    {
        Assets.Scripts.Constants.BonusPoints.Setup();
    }

    [Test]
    public void Score3_SingleDoubleBonus_Score6()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());

        sut.AddNewBonus(new ScoreBonus(2, 1));

        Assert.AreEqual(6, sut.CalculateScore(sequence.ToArray()));
    }


    [Test]
    public void Score3_DoubleHalfBonus_Score6()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());

        sut.AddNewBonus(new ScoreBonus(1.5f, 1));
        sut.AddNewBonus(new ScoreBonus(1.5f, 1));


        Assert.AreEqual(9, sut.CalculateScore(sequence.ToArray()));
    }


    [Test]
    public void Score3_SingleDoubleBonus_Expired_Score3()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());

        sut.AddNewBonus(new ScoreBonus(2, 1));
        sut.CalculateScore(sequence.ToArray());

        // Should be expired now, retry.
        Assert.AreEqual(3, sut.CalculateScore(sequence.ToArray()));
    }

    private static ISquarePiece GetStandardScore()
    {
        var piece = CreatePiece();
        piece.Scoring = new SingleScore();
        return piece;
    }

    private static ISquarePiece GetDoubleScore()
    {
        var piece = CreatePiece();
        piece.Scoring = new MultipliedScore(2);
        return piece;
    }
}
