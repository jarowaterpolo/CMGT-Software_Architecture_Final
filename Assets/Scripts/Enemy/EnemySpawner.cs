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
        [SerializeField]
        private int maxEnemyCount;

        private NewDungeonGenerator _dungeonGen;
        private List<RectInt> _doneRooms = new();
        private bool _isDead;
        private int enemiesAlive;


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
            while (!_isDead && enemiesAlive < maxEnemyCount)
            {
                yield return new WaitForSeconds(spawnDelay);
                var randomRoom = _doneRooms[Random.Range(1, _doneRooms.Count)];
                Vector3 spawnPos = new(randomRoom.x + randomRoom.width / 2, 1, randomRoom.y + randomRoom.height / 2);
                var randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                Instantiate(randomEnemyPrefab, spawnPos, Quaternion.identity, enemyParent);
                enemiesAlive++;
            }
            yield return null;
            StartCoroutine(SpawnEnemies());
        }

        void SetIsDead(bool isDead)
        {
            _isDead = isDead;
        }

        void changeEnemyAliveCount(EventData eventData)
        {
            enemiesAlive--;
        }

        private void OnEnable()
        {
            PlayerHP.onDeath += SetIsDead;
            EventBus<EnemyDieEventData>.OnEventPublished += changeEnemyAliveCount; 
        }

        private void OnDisable()
        {
            PlayerHP.onDeath -= SetIsDead;
            EventBus<EnemyDieEventData>.OnEventPublished -= changeEnemyAliveCount;
        }
    }
}
