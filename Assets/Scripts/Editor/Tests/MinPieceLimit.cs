using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Assets.Scripts.Workers.Score_and_Limits;
using System.Collections.Generic;

public class MinPieceLimit
{

	[Test]
	public void True_Limit3_Pieces3()
    {
        var sut = new MinSequenceLimit(3);

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(new SquarePiece());
        sequence.AddLast(new SquarePiece());
        sequence.AddLast(new SquarePiece());
        sut.SequenceCompleted(sequence);

        Assert.IsFalse(sut.ViolatedRestriction());
    }

    [Test]
    public void True_Limit3_Pieces4()
    {
        var sut = new MinSequenceLimit(3);

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(new SquarePiece());
        sequence.AddLast(new SquarePiece());
        sequence.AddLast(new SquarePiece());
        sequence.AddLast(new SquarePiece());
        sut.SequenceCompleted(sequence);

        Assert.IsFalse(sut.ViolatedRestriction());
    }

    [Test]
    public void False_Limit3_Pieces2()
    {
        var sut = new MinSequenceLimit(3);

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(new SquarePiece());
        sequence.AddLast(new SquarePiece());
        sut.SequenceCompleted(sequence);

        Assert.IsTrue(sut.ViolatedRestriction());
    }
}
