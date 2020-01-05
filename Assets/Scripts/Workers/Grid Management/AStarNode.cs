using UnityEngine;

namespace Assets.Scripts.Workers.Grid_Management
{
    public class AStarNode
    {
        public Vector2Int Location;
        public Vector2Int Parent;
        public float Distance;
    }
}
