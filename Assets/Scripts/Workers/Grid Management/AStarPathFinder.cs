using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Workers.Grid_Management
{
    public class AStarPathFinder : IPathFinder
    {       
        public List<Vector2Int> FindPath(Vector2Int from, Vector2Int target, IRestriction restriction)
        {
            var route = new Stack<AStarNode>();
            var visited = new Stack<AStarNode>();
            var startingIndicator = new Vector2Int(-1, -1);
            bool targetReached = false;

            route.Push(new AStarNode() { Location = from, Parent = startingIndicator, Distance = Vector2Int.Distance(from, target) });

            var r = new AStarNode();
            while (route.Count > 0)
            {
                r = route.Pop();

                if (r.Location == target)
                {
                    targetReached = true;
                    break;
                }

                var piece = PieceManager.Instance.GetPiece(r.Location.x, r.Location.y);
                var neighbours = AIHelpers.GetNeighbours(restriction, piece);

                foreach (var neighbour in neighbours)
                {
                    if (visited.Any(x => x.Location == neighbour.Position))
                    {
                        continue;
                    }

                    route.Push(new AStarNode() { Location = neighbour.Position, Parent = r.Location, Distance = Vector2Int.Distance(neighbour.Position, target) });
                }

                route.OrderBy(x => x.Distance);
                visited.Push(r);
            }

            if (!targetReached)
            {
                return new List<Vector2Int>();
            }

            var bestRoute = new Stack<Vector2Int>(); // Store our best route		
            bestRoute.Clear();
            bestRoute.Push(target); // Add the target location (it will appear at the bottom so it'll be the last location to visit)
            var searchingParent = r.Parent; // Get the parent of the last location we visited(which will be the target location). Got to find its parent.

            // While we haven't reached the target location...
            while (!searchingParent.Equals(startingIndicator))
            {
                // Go through each location and compare it against the parent we are looking for
                while (!visited.Peek().Location.Equals(searchingParent))
                {
                    visited.Pop();
                }
                // Found the parent! Now look for it's parent and store it's location
                searchingParent = visited.Peek().Parent;
                bestRoute.Push(visited.Pop().Location);
            }

            // We now have a list of of the tiles we need to move to to get to the target location. Return it as an array.
            return bestRoute.ToList();
        }
    }
}
