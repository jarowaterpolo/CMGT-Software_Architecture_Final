namespace JJNDungeonGeneration
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class AddWalls : Generator
    {
        private NewDungeonGenerator dungeonGen;
        private TileMapGenerator tileMapGen;

        [SerializeField]
        private GameObject[] wallPrefabs;

        [SerializeField]
        private Transform dungeonParent;

        private int bottomRight;
        private int topRight;
        private int topLeft;
        private int bottomLeft;

        private RectInt currentLocation;

        private int[,] tileMap;

        private List<CombineInstance> combineInstances = new List<CombineInstance>();

        private void Start()
        {
            dungeonGen = GetComponent<NewDungeonGenerator>();
            tileMapGen = GetComponent<TileMapGenerator>();

            tileMapGen.OnEndGeneration += tileMapGen_OnEndGeneration;
            dungeonGen.OnStartGeneration += dungeonGen_OnStartGeneration;

            if (dungeonParent != null)
            {
                if (!dungeonParent.TryGetComponent<MeshFilter>(out _)) dungeonParent.gameObject.AddComponent<MeshFilter>();
                if (!dungeonParent.TryGetComponent<MeshCollider>(out _)) dungeonParent.gameObject.AddComponent<MeshCollider>();
            }
        }

        private void Update()
        {
            AlgorithmsUtils.DebugRectInt(currentLocation, Color.blue);
        }

        private void dungeonGen_OnStartGeneration()
        {
            foreach (Transform child in dungeonParent)
            {
                Destroy(child.gameObject);
            }

            combineInstances.Clear();
            if (dungeonParent.TryGetComponent<MeshCollider>(out var collider))
            {
                collider.sharedMesh = null;
            }
        }
        private void tileMapGen_OnEndGeneration()
        {
            tileMap = tileMapGen.GetTileMap();
            StartCoroutine(MarchSquares());
        }

        private IEnumerator MarchSquares()
        {
            DispatchOnStartGenerationEvent();

            int rows = tileMap.GetLength(0);
            int cols = tileMap.GetLength(1);
            int index = 0;
            var value = 0;

            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < cols - 1; j++)
                {
                    currentLocation = new(j, i, 2, 2);
                    bottomRight = tileMap[i, j + 1];
                    topRight = tileMap[i + 1, j + 1];
                    topLeft = tileMap[i + 1, j];
                    bottomLeft = tileMap[i, j];

                    value = (1 * bottomRight + 2 * topRight + 4 * topLeft + 8 * bottomLeft);

                    SpawnWall(value, new(i + 1, j + 1));

                    if (waitingType != WaitingType.Instant) yield return CustomWait(waitingType, splitDelay);
                    index++;
                }
            }
            GenerateCombinedCollider();
            currentLocation = new();
            DispatchOnEndGenerationEvent();
        }

        private void GenerateCombinedCollider()
        {
            if (combineInstances.Count == 0)
            {
                Debug.LogWarning("Cannot combine colliders: No wall meshes were recorded! Check if prefabs have MeshFilters.");
                return;
            }

            Mesh combinedMesh = new Mesh();
            combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

            // FIX: Combine meshes using the local coordinate space of the parent
            combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true, false);

            if (dungeonParent.TryGetComponent<MeshCollider>(out var meshCollider))
            {
                meshCollider.sharedMesh = combinedMesh;
                Debug.Log($"Successfully built combined wall collider with {combineInstances.Count} sub-meshes!");
            }

            combineInstances.Clear();
        }

        private void SpawnWall(int value, Vector2Int pos)
        {
            var wall = wallPrefabs[value];
            if (wall == null) return;
            var wallInstance = Instantiate(wall, new Vector3(pos.y, 0, pos.x), Quaternion.identity, dungeonParent);


            MeshFilter[] filters = wallInstance.GetComponentsInChildren<MeshFilter>();

            foreach (var filter in filters)
            {
                if (filter != null && filter.sharedMesh != null)
                {
                    CombineInstance combine = new CombineInstance
                    {
                        mesh = filter.sharedMesh,
                        // FIX: Matrix tracking relative to the dungeonParent instead of general world space
                        transform = dungeonParent.worldToLocalMatrix * filter.transform.localToWorldMatrix
                    };
                    combineInstances.Add(combine);
                }
            }

            // Cleanup all individual components down the chain
            foreach (var col in wallInstance.GetComponentsInChildren<Collider>())
            {
                Destroy(col);
            }
        }
    }
}