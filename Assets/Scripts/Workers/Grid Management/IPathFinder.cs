using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.Grid_Management
{
    public interface IPathFinder
    {
        List<Vector2Int> FindPath(Vector2Int from, Vector2Int to, IRestriction restriction);
    }
}