using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Destruction
{
    public class DestroyTriggerFall : IPieceDestroy
    {
        protected SquarePiece _squarePiece;

        public bool ToBeDestroyed { get; private set; }

        private PieceFallLerp Lerp
        {
            get
            {
                var lerp = _squarePiece.GetComponent<PieceFallLerp>();
                if (lerp == null)
                {
                    lerp = _squarePiece.gameObject.AddComponent<PieceFallLerp>();
                }

                return lerp;
            }
        }
        

        public DestroyTriggerFall (SquarePiece squarePiece)
        {
            _squarePiece = squarePiece;
            Lerp.LerpCompleted += Lerp_LerpCompleted;
        }

        ~DestroyTriggerFall()
        {
            Lerp.LerpCompleted -= Lerp_LerpCompleted;
        }

        public virtual void OnDestroy()
        {
            _squarePiece.gameObject.SetActive(false);
            PieceManager.Instance.Pieces.Remove(_squarePiece);
            CheckForAbove();

            PieceDropper.Instance.CheckForEmptySlots();
        }

        protected void CheckForAbove()
        {
            ISquarePiece[] pieces = PieceManager.Instance.GetPiecesAbove(_squarePiece.Position.x, _squarePiece.Position.y);

            var sorted = pieces.OrderByDescending(x => x.Position.y);
            
            foreach (var p in sorted)
            {
                var dest = p.DestroyPieceHandler as DestroyTriggerFall;
                if (dest == null || dest.ToBeDestroyed)
                {
                    continue;
                }

                if (!PieceManager.Instance.SlotExists(_squarePiece.Position))
                {
                    continue;
                }

                dest.FallTo(_squarePiece.Position.y);
                break;
            }            
        }

        public virtual void FallTo(int y)
        {
           float worldPos = PieceManager.Instance.YPositions[y];            
            
            CheckForAbove();
          
            Lerp.Setup(new Vector3(_squarePiece.transform.position.x, worldPos));

            _squarePiece.Position = new Vector2Int(_squarePiece.Position.x, y);
        }

        private void Lerp_LerpCompleted()
        {
            if (!PieceManager.Instance.SlotExists(_squarePiece.Position))
            {
                _squarePiece.DestroyPiece();
            }
        }   
       
        public void OnPressed()
        {            
        }

        public virtual void Reset()
        {
            ToBeDestroyed = false;
        }

        public void NotifyOfDestroy()
        {
            ToBeDestroyed = true;

            var colour = _squarePiece.Sprite.texture.GetTextureColour();
            GameResources.PlayEffect("Piece Destroy", _squarePiece.transform.position, colour);
        }
    }
}
