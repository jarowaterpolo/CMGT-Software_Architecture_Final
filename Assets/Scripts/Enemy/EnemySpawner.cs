using JJNDungeonGeneration;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJN.PathFinding
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        private float spawnDelay;
        [SerializeField]
        private Transform enemyParent;

        private NewDungeonGenerator dungeonGen;
        private List<RectInt> doneRooms = new();


        void Start()
        {
            dungeonGen = GameObject.FindGameObjectWithTag("DungeonGenerator").GetComponent<NewDungeonGenerator>();
        }

        public void StartSpawningEnemies()
        {
            GetDoneRooms();
            StartCoroutine(SpawnEnemies());
        }
        private void GetDoneRooms()
        {
            doneRooms = dungeonGen.doneRooms;
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);
                var randomRoom = doneRooms[Random.Range(1, doneRooms.Count)];
                Vector3 spawnPos = new(randomRoom.x + randomRoom.width / 2, 1, randomRoom.y + randomRoom.height / 2);
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemyParent);
            }
        }
    }
}
