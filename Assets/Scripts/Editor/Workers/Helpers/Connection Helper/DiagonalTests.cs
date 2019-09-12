using Assets.Scripts.Workers.Helpers;
using NUnit.Framework;
using UnityEngine;
using static Assets.Scripts.Workers.TestHelpers;

[Category("Helpers")]
public class DiagonalTests
{
    [Test]
    public void PieceIsDiagonal_Test1()
    {
        var piece1 = CreatePiece();
        piece1.Position = new Vector2Int(1, 1);

        var piece2 = CreatePiece();
        piece2.Position = new Vector2Int(2, 2);

        Assert.IsTrue(ConnectionHelper.DiagonalConnection(piece1, piece2));
    }

    [Test]
    public void PieceIsDiagonal_Test2()
    {
        var piece1 = CreatePiece();
        piece1.Position = new Vector2Int(5, 4);

        var piece2 = CreatePiece();
        piece2.Position = new Vector2Int(4, 5);

        Assert.IsTrue(ConnectionHelper.DiagonalConnection(piece1, piece2));
    }

    [Test]
    public void PieceIsNotDiagonal_Test1()
    {
        var piece1 = CreatePiece();
        piece1.Position = new Vector2Int(1, 1);

        var piece2 = CreatePiece();
        piece2.Position = new Vector2Int(1, 2);

        Assert.IsFalse(ConnectionHelper.DiagonalConnection(piece1, piece2));
    }


    [Test]
    public void PieceIsNotDiagonal_Test2()
    {
        var piece1 = CreatePiece();
        piece1.Position = new Vector2Int(1, 1);

        var piece2 = CreatePiece();
        piece2.Position = new Vector2Int(0, 1);

        Assert.IsFalse(ConnectionHelper.DiagonalConnection(piece1, piece2));
    }
}