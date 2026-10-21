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

        [SerializeField]
        private int MaxHPGainBasedOnLevelUp;
        [SerializeField]
        private HP_ScriptableObject hpObject;

        public void OnEnemyKilled(EventData eventData)
        {
            if (currentLevel.value <= xpNeededForEachLevel.Length)
            {
                EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;

                currentXP.value += enemyDieEventData.enemy.XP;

                int previousLevel = currentLevel.value;

                while (currentXP.value >= xpNeededForEachLevel[currentLevel.value - 1])
                {
                    currentXP.value -= xpNeededForEachLevel[currentLevel.value - 1];

                    currentLevel.value++;

                    if (currentLevel.value > xpNeededForEachLevel.Length)
                    {
                        break;
                    }
                }
            }
        }

        public void GainMaxHPOnLevelUp(int num)
        {
            hpObject.Max_HP_value += MaxHPGainBasedOnLevelUp;
            Debug.Log($"tried to increase player max hp by {MaxHPGainBasedOnLevelUp}");
        }

        void GainHPOnLevelUP()
        {
            hpObject.HP_value += MaxHPGainBasedOnLevelUp;
            Debug.Log($"tried to increase player hp by {MaxHPGainBasedOnLevelUp}");
        }


        private void OnEnable()
        {
            currentLevel.onValueChanged += GainMaxHPOnLevelUp;
            hpObject.onMaxHPValueChanged += GainHPOnLevelUP;
            EventBus<EnemyDieEventData>.OnEventPublished += OnEnemyKilled;
        }

        private void OnDisable()
        {
            currentLevel.onValueChanged -= GainMaxHPOnLevelUp;
            hpObject.onMaxHPValueChanged -= GainHPOnLevelUP;
            EventBus<EnemyDieEventData>.OnEventPublished -= OnEnemyKilled;
        }
    }
}
