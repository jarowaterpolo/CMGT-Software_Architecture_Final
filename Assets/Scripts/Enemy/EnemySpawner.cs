using JJN.QuestSystem.Quests;
using JJN.ScriptableObjects;
using JJNDungeonGeneration;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJN.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private Stage[] stages;
        [SerializeField]
        private float spawnDelay;
        [SerializeField]
        private Transform enemyParent;
        [SerializeField]
        private int maxEnemyCount;
        [SerializeField]
        private IntValueWithAction stage;

        private NewDungeonGenerator _dungeonGen;
        private List<RectInt> _doneRooms = new();
        private bool _isDead;
        private int enemiesAlive;
        private bool bossSpawned;


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

                var enemyPrefabLength = stages[stage.value].enemyPrefabs.Length;

                int randomNumber;
                GameObject enemyPrefab;

                if (!bossSpawned)
                {
                    randomNumber = Random.Range(0, enemyPrefabLength + 1);
                }
                else
                {
                    randomNumber = Random.Range(0, enemyPrefabLength);
                }

                if (randomNumber >= enemyPrefabLength)
                {
                    enemyPrefab = stages[stage.value].bossPrefab;
                    bossSpawned = true;
                }
                else
                {
                    enemyPrefab = stages[stage.value].enemyPrefabs[Random.Range(0, stages[stage.value].enemyPrefabs.Length)];
                }

                Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemyParent);
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

        void IncreaseStage(EventData eventData)
        {
            stage.value++;
        }

        private void OnEnable()
        {
            PlayerHP.onDeath += SetIsDead;
            EventBus<EnemyDieEventData>.OnEventPublished += changeEnemyAliveCount; 
            EventBus<AdvanceStageEventData>.OnEventPublished += IncreaseStage;
        }

        private void OnDisable()
        {
            PlayerHP.onDeath -= SetIsDead;
            EventBus<EnemyDieEventData>.OnEventPublished -= changeEnemyAliveCount;
            EventBus<AdvanceStageEventData>.OnEventPublished -= IncreaseStage;
        }
    }
}
