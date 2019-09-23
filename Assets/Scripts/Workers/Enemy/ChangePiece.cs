using Assets.Scripts.Workers.IO.Data_Entities;
using static PieceFactory;

namespace Assets.Scripts.Workers.Enemy
{
    class ChangePiece : PieceSelectionRage
    {
        public PieceTypes NewPieceType = PieceTypes.Empty;

        protected override void InvokeRageAction(ISquarePiece piece)
        {
            GameResources.PlayEffect("Piece Change", piece.transform.position);

            var newPiece = PieceFactory.Instance.CreateSquarePiece(NewPieceType, false);

            newPiece.transform.position = piece.transform.position;
            newPiece.transform.parent = piece.transform.parent;
            newPiece.GetComponent<SquarePiece>().Position = piece.Position;

            PieceController.Pieces.Remove(piece);
            PieceController.Pieces.Add(newPiece.GetComponent<SquarePiece>());

            piece.gameObject.SetActive(false);
        }
    }
}
