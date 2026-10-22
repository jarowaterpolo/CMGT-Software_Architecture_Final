using JJN.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerSystem.Values
{
    public class PlayerXPController : MonoBehaviour
    {
        public int[] xpNeededForEachLevel; 
        public IntValueWithAction currentXP; 
        public IntValueWithAction currentLevel; 

        public static event Action<int> levelUp;

        public void OnEnemyKilled(EventData eventData)
        {
            if (currentLevel.value <= xpNeededForEachLevel.Length)
            {
                EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;

                currentXP.value += enemyDieEventData.enemy.enemyData.XP;

                int previousLevel = currentLevel.value;

                while (currentXP.value >= xpNeededForEachLevel[currentLevel.value - 1])
                {
                    currentXP.value -= xpNeededForEachLevel[currentLevel.value - 1];

                    currentLevel.value++;
                    levelUp?.Invoke(currentLevel.value);

                    if (currentLevel.value > xpNeededForEachLevel.Length)
                    {
                        break;
                    }
                }
            }
        }



        private void OnEnable()
        {
            EventBus<EnemyDieEventData>.OnEventPublished += OnEnemyKilled;
        }

        private void OnDisable()
        {
            EventBus<EnemyDieEventData>.OnEventPublished -= OnEnemyKilled;
        }
    }
}
