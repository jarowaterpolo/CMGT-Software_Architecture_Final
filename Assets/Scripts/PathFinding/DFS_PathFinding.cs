using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JJN.PathFinding
{
    public class DFS_PathFinding : PathFindingAlgorithm
    {
        public override List<Vector3> DFS(Vector3 start, Vector3 end)
        {
            //Use this "Discovered" list to see the nodes in the visual debugging used on OnDrawGizmos()
            Discovered.Clear();

            Stack<Vector3> ToDo = new();
            Dictionary<Vector3, Vector3> parentMap = new();
            Vector3 currentNode = start;

            ToDo.Push(currentNode);
            Discovered.Add(currentNode);

            while (ToDo.Count > 0)
            {
                currentNode = ToDo.Pop();

                if (currentNode == end)
                {
                    return ReconstructPath(parentMap, start, end);
                }

                var neighbors = Graph.GetNeighbors(currentNode);

                foreach (Vector3 neighbor in neighbors)
                {
                    if (!Discovered.Contains(neighbor))
                    {
                        Discovered.Add(neighbor);
                        ToDo.Push(neighbor);
                        parentMap[neighbor] = currentNode;
                    }
                }

            }

            return new List<Vector3>(); // No Path found
        }
    }
}
