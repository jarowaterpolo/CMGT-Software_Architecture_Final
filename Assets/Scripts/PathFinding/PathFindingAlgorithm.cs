using JJNDungeonGeneration;
using System.Collections.Generic;
using UnityEngine;

namespace JJN.PathFinding
{
    public class PathFindingAlgorithm : MonoBehaviour
    {
        protected HashSet<Vector3> Discovered = new HashSet<Vector3>();

        public Graph<Vector3> Graph;

        private Vector3 offset = new(0,1,0);

        public void SetGraph(Graph<Vector3> graph)
        {
            Graph = graph;
        }
        public float Cost(Vector3 from, Vector3 to)
        {
            return Vector3.Distance(from, to);
        }
        public float Heuristic(Vector3 from, Vector3 to)
        {
            return Vector3.Distance(from, to);
        }
        public List<Vector3> ReconstructPath(Dictionary<Vector3, Vector3> parentMap, Vector3 start, Vector3 end)
        {
            List<Vector3> path = new List<Vector3>();
            Vector3 currentNode = end;

            while (currentNode != start)
            {
                path.Add(currentNode + offset);
                currentNode = parentMap[currentNode];
            }

            path.Add(start + offset);
            path.Reverse();
            return path;
        }
        public virtual List<Vector3> AStar(Vector3 start, Vector3 end)
        {
            return null;
        }
        public virtual List<Vector3> Dijkstra(Vector3 start, Vector3 end)
        {
            return null;
        }
        public virtual List<Vector3> BFS(Vector3 start, Vector3 end)
        {
            return null;
        }
        public virtual List<Vector3> DFS(Vector3 start, Vector3 end)
        {
            return null;
        }
    }
}
