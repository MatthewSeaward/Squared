using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Grid_Management
{
    public interface IBestMoveChecker
    {
        List<ISquarePiece> GetBestMove(IRestriction restriction);
        List<ISquarePiece> GetBestMove();
    }
}
