﻿using Assets.Scripts.Workers.IO.Data_Entities;
using static PieceBuilderDirector;

namespace Assets.Scripts.Workers.Helpers
{
    public static class SquarePieceHelpers
    {
        public static void ChangePiece(ISquarePiece piece, PieceTypes newPieceType)
        {
            GameResources.PlayEffect("Piece Change", piece.transform.position);

            var newPiece = PieceBuilderDirector.Instance.CreateSquarePiece(newPieceType, false);

            newPiece.transform.position = piece.transform.position;
            newPiece.transform.parent = piece.transform.parent;
            newPiece.GetComponent<SquarePiece>().Position = piece.Position;

            PieceController.Pieces.Remove(piece);
            PieceController.Pieces.Add(newPiece.GetComponent<SquarePiece>());

            piece.gameObject.SetActive(false);
        }
    }
}
