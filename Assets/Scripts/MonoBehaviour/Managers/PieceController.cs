using Assets;
using Assets.Scripts.Workers.Piece_Effects.SwapEffects;
using System.Collections.Generic;
using System.Linq;

public class PieceController 
{
    public static List<SquarePiece> Pieces { get; private set; }
    public static float[] YPositions { get; private set; }
    public static float[] XPositions { get; private set; }

    public static int NumberOfRows => YPositions.Length;
    public static int NumberOfColumns => XPositions.Length;

    internal static bool HasEmptySlotInColumn(int x, ref int y)
    {
        for (int i = 0; i < NumberOfColumns; i++)
        {
            if (IsEmptySlot(x, i))
            {
                y = i;
                return true;
            }
        }
        return false;
    }
    
    internal static void Setup(List<SquarePiece> Pieces, float[] YPositions, float[] XPositions)
    {
        PieceController.Pieces = Pieces;
        PieceController.YPositions = YPositions;
        PieceController.XPositions = XPositions;
    }

    public static bool IsEmptySlot(int x, int y)
    {
        var pattern = LevelManager.Instance.SelectedLevel.Pattern;

        if (y >= pattern.Length || y < 0)
        {
            return true;
        }

        var row = pattern[y];
        if (x >= row.Length || x < 0)
        {
            return false;
        }

        var cell = row[x];

        if (cell == '-')
        {
            return false;
        }

        var currentPiece = GetPiece(x, y);
        if (currentPiece == null || !currentPiece.gameObject.activeInHierarchy)
        {
            return true;
        }

        if (currentPiece.SwapEffect is LockedSwap)
        {
            return IsEmptySlot(x, y + 1);
        }

        return false;
            
    }

    internal static bool AtTop(int x, int y)
    {
        var piecesAbove = Pieces.Where(p => p.Position.x == x && p.Position.y < y);
        foreach(var piece in piecesAbove)
        {
            if (piece != null)
            {
                return false;
            }
        }
        return true;
    }


    internal static SquarePiece GetPiece(int x, int y)
    {
        return Pieces.FirstOrDefault(p => p.Position.x == x && p.Position.y == y);
    }

    internal static SquarePiece[] GetPiecesAbove(int x, int y)
    {
        return Pieces.Where(p => p.Position.x == x && p.Position.y < y).ToArray();
    }

    internal static SquarePiece GetPieceAbove(int x, int y)
    {
        return Pieces.Where(p => p.Position.x == x && p.Position.y < y).Min();
    }
}
