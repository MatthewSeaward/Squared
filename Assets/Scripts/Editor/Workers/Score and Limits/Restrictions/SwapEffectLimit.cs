using Assets.Scripts.Workers.Piece_Effects.Destruction;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static Assets.Scripts.Workers.Helpers.TestHelpers;

[Category("Restrictions")]
public class SwapEffectLimit
{

    [Test]
    public void Violated_False_NoLockedPieces()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.SwapEffectLimit();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sequence.AddLast(CreatePiece());
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsFalse(sut.ViolatedRestriction());
    }

    [Test]
    public void Violated_True_LockedPieceUsed()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.SwapEffectLimit();

        var lockedPiece = CreatePiece();
        lockedPiece.DestroyPieceHandler = new Assets.Scripts.Workers.Piece_Effects.Destruction.LockedSwap(lockedPiece);

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece());
        sequence.AddLast(lockedPiece);
        sequence.AddLast(CreatePiece());
        sut.SequenceCompleted(sequence.ToArray());

        Assert.IsTrue(sut.ViolatedRestriction());
    }

}
