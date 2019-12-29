using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Score_and_Limits;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static Assets.Scripts.Workers.Helpers.TestHelpers;

[Category("Score Calculation")]
public class StandardScoreCalculationWithBonus
{
    [OneTimeSetUp]
    public void TestStart()
    {
        Assets.Scripts.Constants.BonusPoints.Points = new List<(int, int)>()
        {
            (2, 2),
            (3, 3),
            (4, 5)
        };
    }

    [Test]
    public void Score1SinglePieces_Bonus0_Score1()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetStandardScore());

        Assert.AreEqual(1, sut.CalculateScore(sequence.ToArray()));
    }

    [Test]
    public void Score3SinglePieces_Bonus2_Score4()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        
        Assert.AreEqual(4, sut.CalculateScore(sequence.ToArray()));
    }

    [Test]
    public void Score3SinglePieces_Bonus3_Score6()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());

        Assert.AreEqual(6, sut.CalculateScore(sequence.ToArray()));
    }

    [Test]
    public void Score4SinglePieces_Bonus5_Score9()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());

        Assert.AreEqual(9, sut.CalculateScore(sequence.ToArray()));
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