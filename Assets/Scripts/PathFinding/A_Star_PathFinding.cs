using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JJN.PathFinding
{
    public class A_Star_PathFinding : PathFindingAlgorithm
    {
        public override List<Vector3> AStar(Vector3 start, Vector3 end)
        {
            Debug.Log("start A* pathfinding");
            //Use this "Discovered" list to see the nodes in the visual debugging used on OnDrawGizmos()
            Discovered.Clear();

            Dictionary<Vector3, float> costMap = new();
            Dictionary<Vector3, Vector3> parentMap = new();
            List<(Vector3 node, float cost)> ToDo = new();

            costMap[start] = 0;
            ToDo.Add((start, 0));
            Discovered.Add(start);

            while (ToDo.Count > 0)
            {
                ToDo = ToDo.OrderByDescending(node => node.cost).ToList();

                var currentNode = ToDo[ToDo.Count - 1].node;
                ToDo.RemoveAt(ToDo.Count - 1);

                if (currentNode == end)
                {
                    return ReconstructPath(parentMap, start, end);
                }

                var neighbors = Graph.GetNeighbors(currentNode);

                foreach (Vector3 neighbor in neighbors)
                {
                    var newCost = costMap[currentNode] + Cost(currentNode, neighbor);

                    if (!costMap.ContainsKey(neighbor) || newCost < costMap[neighbor])
                    {
                        Discovered.Add(neighbor);

                        costMap[neighbor] = newCost;
                        parentMap[neighbor] = currentNode;
                        ToDo.Add((neighbor, newCost + Heuristic(neighbor, end)));
                    }
                }

            }

            /* */
            return new List<Vector3>(); // No Path found
        }
    }
}
