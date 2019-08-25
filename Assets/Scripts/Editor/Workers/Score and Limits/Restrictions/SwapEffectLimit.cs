using Assets.Scripts.Workers.Piece_Effects.SwapEffects;
using NUnit.Framework;
using System.Collections.Generic;
using static Assets.Scripts.Workers.TestHelpers;

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
        sut.SequenceCompleted(sequence);

        Assert.IsFalse(sut.ViolatedRestriction());
    }

    [Test]
    public void Violated_True_LockedPieceUsed()
    {
        var sut = new Assets.Scripts.Workers.Score_and_Limits.SwapEffectLimit();

        var lockedPiece = CreatePiece();
        lockedPiece.SwapEffect = new LockedSwap();

        var sequence = new LinkedList<ISquarePiece>();
        sequence.AddLast(CreatePiece());
        sequence.AddLast(lockedPiece);
        sequence.AddLast(CreatePiece());
        sut.SequenceCompleted(sequence);

        Assert.IsTrue(sut.ViolatedRestriction());
    }

}
