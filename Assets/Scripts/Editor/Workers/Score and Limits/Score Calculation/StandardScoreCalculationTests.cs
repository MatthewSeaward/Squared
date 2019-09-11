using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Score_and_Limits;
using NUnit.Framework;
using System.Collections.Generic;
using static Assets.Scripts.Workers.TestHelpers;

[Category("Score Calculation")]
public class StandardScoreCalculationTests
{
    [OneTimeSetUp]
    public void TestStart()
    {
        Assets.Scripts.Constants.BonusPoints.Setup();
    }

    [Test]
    public void Score3SinglePieces_Score3()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        
        Assert.AreEqual(3, sut.CalculateScore(sequence));
    }


    [Test]
    public void Score5SinglePieces_GetBonus_Score5()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());

        Assert.AreEqual(8, sut.CalculateScore(sequence));
    }

    [Test]
    public void Score0SinglePieces_Score0()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();

        Assert.AreEqual(0, sut.CalculateScore(sequence));
    }

    [Test]
    public void Score2SinglePieces1Double_Score4()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetDoubleScore());

        Assert.AreEqual(4, sut.CalculateScore(sequence));
    }


    [Test]
    public void Score3SinglePieces1Double1Single_Score6()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetDoubleScore());
        sequence.AddLast(GetStandardScore());


        Assert.AreEqual(6, sut.CalculateScore(sequence));
    }

    [Test]
    public void Score31Double3Single_Score6()
    {
        var sut = new StandardScoreCalculator();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(GetDoubleScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());
        sequence.AddLast(GetStandardScore());


        Assert.AreEqual(6, sut.CalculateScore(sequence));
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
