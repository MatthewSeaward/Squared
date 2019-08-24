using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Workers.Piece_Effects.Interfaces
{
    public interface IPieceDestroy
    {
        void OnPressed();
        void Update();
        void OnDestroy();
        void NotifyOfDestroy();
        void Reset();
    }
}
