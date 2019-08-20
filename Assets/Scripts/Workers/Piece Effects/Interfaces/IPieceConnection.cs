using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Workers.Piece_Effects.Interfaces
{
    public interface IPieceConnection
    {
        bool ConnectionValid(ISquarePiece selectedPiece);
    }
}
