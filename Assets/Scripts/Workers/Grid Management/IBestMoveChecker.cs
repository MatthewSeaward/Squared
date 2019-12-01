using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Grid_Management
{
    public enum SearchResult { Success, NoMoves, TimeOut };

    public interface IBestMoveChecker
    {
        (SearchResult Result, List<ISquarePiece> Move) GetBestMove(IRestriction restriction);
        (SearchResult Result, List<ISquarePiece> Move) GetBestMove();
    }
}
