using Assets.Scripts.Workers.Piece_Effects.Destruction;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceController 
{
    public static List<ISquarePiece> Pieces { get; private set; }
    public static float[] YPositions { get; private set; }
    public static float[] XPositions { get; private set; }

    private static List<Vector2Int> AvaiableSlots = new List<Vector2Int>();

    public static int NumberOfRows => YPositions.Length;
    public static int NumberOfColumns => XPositions.Length;

    internal static bool HasEmptySlotInColumn(int x, ref int y)
    {
        for (int i = 0; i < NumberOfRows; i++)
        {
            if (IsEmptySlot(x, i))
            {
                y = i;
                return true;
            }
        }
        return false;
    }

 
    public static void Setup(List<ISquarePiece> Pieces, float[] XPositions, float[] YPositions)
    {
        PieceController.Pieces = Pieces;
        PieceController.YPositions = YPositions;
        PieceController.XPositions = XPositions;

        AvaiableSlots.Clear();
        foreach(var piece in Pieces)
        {
            AvaiableSlots.Add(piece.Position);
        }
    }

    internal static void AddNewPiece(ISquarePiece newPiece)
    {
        Pieces.Add(newPiece);
        AvaiableSlots.Add(newPiece.Position);
    }

    internal static void RemovePiece(ISquarePiece piece)
    {
        Pieces.Remove(piece);
        AvaiableSlots.Remove(piece.Position);
    }

    public static bool IsEmptySlot(int x, int y)
    {        
       if (!AvaiableSlots.Contains(new Vector2Int(x, y)))
       {
            return false;
       }

        var currentPiece = GetPiece(x, y);
        if (currentPiece == null || !currentPiece.gameObject.activeInHierarchy)
        {
            return true;
        }

        if (currentPiece.DestroyPieceHandler is LockedSwap)
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

    internal static bool EmptyColumn(int column)
    {
        for (int row = 0; row < NumberOfRows; row++)
        {
            if (!AvaiableSlots.Contains(new Vector2Int(row, column)))
            {
                return false;
            }
        }
        return true;
    }
      

    internal static ISquarePiece GetPiece(int x, int y)
    {
        return Pieces.FirstOrDefault(p => p.Position.x == x && p.Position.y == y);
    }

    internal static ISquarePiece[] GetPiecesAbove(int x, int y)
    {
        return Pieces.Where(p => p.Position.x == x && p.Position.y < y).ToArray();
    }

    internal static ISquarePiece GetPieceAbove(int x, int y)
    {
        return Pieces.Where(p => p.Position.x == x && p.Position.y < y).Min();
    }
}
