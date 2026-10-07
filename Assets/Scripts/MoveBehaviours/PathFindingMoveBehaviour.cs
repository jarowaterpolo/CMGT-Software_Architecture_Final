using JJN.PathFinding;
using JJNDungeonGeneration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerSystem.Movement
{
    public enum Algorithms
    {
        BFS,
        DFS,
        Dijkstra,
        AStar
    }
    public class PathFindingMoveBehaviour : MoveBehaviour
    {
        private Vector3 startNode;
        private Vector3 endNode;

        public List<Vector3> Path = new List<Vector3>();

        protected Graph<Vector3> Graph;
        protected TileMapGraph tileMapGraph;


        public Algorithms algorithm = Algorithms.BFS;

        [Header("Path Finding Algorithms")]
        [SerializeField]
        private PathFindingAlgorithm currentPathFindingAlgorithm;

        [SerializeField]
        private float Speed = 5f;

        private bool isMoving = false;

        private void Start()
        {
            GetGraph();            
        }
        //Pathfollower
        public override void SetTargetPosition(Vector3 targetPos)
        {
            GoToDestination(targetPos);
        }
        public void GoToDestination(Vector3 destination)
        {
            if (isMoving)
            {
                StopAllCoroutines();
                isMoving = false;
            }

            StartCoroutine(FollowPathCoroutine(CalculatePath(transform.position, destination)));
        }

        IEnumerator FollowPathCoroutine(List<Vector3> path)
        {
            if (path == null || path.Count == 0)
            {
                Debug.Log("No Path found");
                yield break;
            }
            isMoving = true;
            for (int i = 0; i < path.Count; i++)
            {
                Vector3 target = path[i];
                // Move towards the target position
                while (Vector3.Distance(transform.position, target) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * Speed);
                    yield return null;
                }

                //Debug.Log($"Reached target: {target}");
            }
            isMoving = false;
        }

        //Pathfinder
        public List<Vector3> CalculatePath(Vector3 from, Vector3 to)
        {
            Debug.Log($"go from {from} to {to}");

            startNode = GetClosestNodeToPosition(from);
            endNode = GetClosestNodeToPosition(to);

            List<Vector3> shortestPath = new List<Vector3>();

            currentPathFindingAlgorithm.SetGraph(Graph);

            switch (algorithm)
            {
                case Algorithms.BFS:
                    shortestPath = currentPathFindingAlgorithm.BFS(startNode, endNode);
                    break;
                case Algorithms.DFS:
                    shortestPath = currentPathFindingAlgorithm.DFS(startNode, endNode);
                    break;
                case Algorithms.Dijkstra:
                    shortestPath = currentPathFindingAlgorithm.Dijkstra(startNode, endNode);
                    break;
                case Algorithms.AStar:
                    shortestPath = currentPathFindingAlgorithm.AStar(startNode, endNode);
                    break;
            }

            Path = shortestPath; //Used for drawing the Path

            return shortestPath;
        }
        protected Vector3 GetClosestNodeToPosition(Vector3 position)
        {
            Vector3 closestNode = Vector3.zero;
            float closestDistance = Mathf.Infinity;

            foreach (var node in Graph.GetKeys())
            {
                float dist = (node - position).sqrMagnitude;

                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestNode = node;
                }
            }

            //Find the closest node to the position

            return closestNode;
        }

        public void GetGraph()
        {
            if (Graph == null)
            {
                tileMapGraph = GameObject.FindGameObjectWithTag("DungeonGenerator").GetComponent<TileMapGraph>();
                Graph = tileMapGraph.graphNodes;
            }
        }
    }
}
