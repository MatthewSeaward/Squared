using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Destruction
{
    public class DestroyTriggerFall : IPieceDestroy
    {
        protected SquarePiece _squarePiece;

        public bool TobeDestroyed { get; private set; }

        public DestroyTriggerFall (SquarePiece squarePiece)
        {
            _squarePiece = squarePiece; 
        }

        public virtual void OnDestroy()
        {
            _squarePiece.gameObject.SetActive(false);
            PieceController.Pieces.Remove(_squarePiece);
            CheckForAbove();

            PieceDropper.Instance.CheckForEmptySlots();
        }

        protected void CheckForAbove()
        {
            SquarePiece[] pieces = PieceController.GetPiecesAbove(_squarePiece.Position.x, _squarePiece.Position.y);

            var sorted = pieces.OrderByDescending(x => x.Position.y);
            
            foreach (var p in sorted)
            {
                var dest = p.DestroyPieceHandler as DestroyTriggerFall;
                if (dest == null || dest.TobeDestroyed)
                {
                    continue;
                }

                dest.FallTo(_squarePiece.Position.y);
                break;
            }            
        }

        public virtual void FallTo(int y)
        {
           float worldPos = PieceController.YPositions[y];            
            
            CheckForAbove();            

            Lerp lerp = _squarePiece.GetComponent<Lerp>();
            if (lerp == null) lerp = _squarePiece.gameObject.AddComponent<Lerp>();
            lerp.Setup(new Vector3(_squarePiece.transform.position.x, worldPos));

            _squarePiece.Position = new Vector2Int(_squarePiece.Position.x, y);
        }

        public void Update()
        {
        }      
       
        public void OnPressed()
        {            
        }

        public virtual void Reset()
        {
            TobeDestroyed = false;
        }

        public void NotifyOfDestroy()
        {
            TobeDestroyed = true;

            var colour = _squarePiece.Sprite.texture.GetTextureColour();
            GameResources.PlayEffect("Piece Destroy", _squarePiece.transform.position, colour);
        }
    }
}
