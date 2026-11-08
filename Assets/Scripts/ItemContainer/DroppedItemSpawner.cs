using JJN.Calculation;
using System;
using UnityEngine;

namespace JJN.ItemSystem
{
    public class DroppedItemSpawner : MonoBehaviour
    {
        [SerializeField]
        private DropPile legendary;
        [SerializeField]
        private DropPile epic;
        [SerializeField]
        private DropPile rare;
        [SerializeField]
        private DropPile uncommon;
        [SerializeField]
        private DropPile common;

        public void SpawnDroppedItem(EventData eventData)
        {
            EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;

            var randomNum = 0;
            randomNum = UnityEngine.Random.Range(0, 100);
            if (randomNum < 33)
            {
                string rolledRarity = Calculator.RollRarity();
                //Debug.Log($"your rolled rarity = {rolledRarity}");
                GameObject dropToSpawn = common.dropPile[0];

                switch (rolledRarity)
                {
                    case "Legendary":
                        randomNum = UnityEngine.Random.Range(0, legendary.dropPile.Length);
                        dropToSpawn = legendary.dropPile[randomNum];
                        break;
                    case "Epic":
                        randomNum = UnityEngine.Random.Range(0, epic.dropPile.Length);
                        dropToSpawn = epic.dropPile[randomNum];
                        break;
                    case "Rare":
                        randomNum = UnityEngine.Random.Range(0, rare.dropPile.Length);
                        dropToSpawn = rare.dropPile[randomNum];
                        break;
                    case "Uncommon":
                        randomNum = UnityEngine.Random.Range(0, uncommon.dropPile.Length);
                        dropToSpawn = uncommon.dropPile[randomNum];
                        break;
                    case "Common":
                        randomNum = UnityEngine.Random.Range(0, common.dropPile.Length);
                        dropToSpawn = common.dropPile[randomNum];
                        break;
                }

                Instantiate(dropToSpawn, enemyDieEventData.enemyObject.transform.position + Vector3.up * .5f, Quaternion.identity);
            }
        }

        private void OnEnable()
        {
            EventBus<EnemyDieEventData>.OnEventPublished += SpawnDroppedItem;
        }

        private void OnDisable()
        {
            EventBus<EnemyDieEventData>.OnEventPublished -= SpawnDroppedItem;
        }
    }
}
