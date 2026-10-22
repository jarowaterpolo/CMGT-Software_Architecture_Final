using JJN.QuestSystem.Quests;
using JJN.ScriptableObjects;
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
        private GameObject[] enemyPrefabs;
        [SerializeField]
        private float spawnDelay;
        [SerializeField]
        private Transform enemyParent;

        private NewDungeonGenerator _dungeonGen;
        private List<RectInt> _doneRooms = new();
        private bool _isDead;


        void Start()
        {
            _dungeonGen = GameObject.FindGameObjectWithTag("DungeonGenerator").GetComponent<NewDungeonGenerator>();
        }

        public void StartSpawningEnemies()
        {
            GetDoneRooms();
            StartCoroutine(SpawnEnemies());
        }
        private void GetDoneRooms()
        {
            _doneRooms = _dungeonGen.doneRooms;
        }

        private IEnumerator SpawnEnemies()
        {
            while (!_isDead)
            {
                yield return new WaitForSeconds(spawnDelay);
                var randomRoom = _doneRooms[Random.Range(1, _doneRooms.Count)];
                Vector3 spawnPos = new(randomRoom.x + randomRoom.width / 2, 1, randomRoom.y + randomRoom.height / 2);
                Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity, enemyParent);
            }
            yield return null;
            StartCoroutine(SpawnEnemies());
        }

        void SetIsDead(bool isDead)
        {
            _isDead = isDead;
        }

        private void OnEnable()
        {
            PlayerHP.onDeath += SetIsDead;
        }

        private void OnDisable()
        {
            PlayerHP.onDeath -= SetIsDead;
        }
    }
}
