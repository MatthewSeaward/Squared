using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Managers;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;
using static SquarePiece;

namespace Assets.Scripts.Workers.Helpers
{
    public static class SquarePieceHelpers
    {
        public static void ChangePiece(ISquarePiece piece, PieceTypes newPieceType)
        {
            GameResources.PlayEffect("Piece Change", piece.transform.position);

            var newPiece = Instance.CreateSquarePiece(newPieceType, false);

            newPiece.transform.position = piece.transform.position;
            newPiece.transform.parent = piece.transform.parent;
            newPiece.GetComponent<SquarePiece>().Position = piece.Position;

            PieceManager.Instance.Pieces.Remove(piece);
            PieceManager.Instance.Pieces.Add(newPiece.GetComponent<SquarePiece>());

            piece.gameObject.SetActive(false);
        }

        public static void ChangePieceColour(ISquarePiece piece, Colour newColour)
        {
            GameResources.PlayEffect("Piece Change", piece.transform.position);

            if (newColour == Colour.None)
            {
                newColour = LevelManager.Instance.SelectedLevel.Colours.RandomElement();
            }

            piece.PieceColour = newColour;
            piece.Sprite = GameResources.PieceSprites[((int)newColour).ToString()];
        }
    }
}
