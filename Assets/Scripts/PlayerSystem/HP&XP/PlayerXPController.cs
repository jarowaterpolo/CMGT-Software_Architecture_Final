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
                CheckForLevelUP();
            }
        }
        public void OnQuestComplete(EventData eventData)
        {
            if (currentLevel.value <= xpNeededForEachLevel.Length)
            {
                QuestEventData questEventData = (QuestEventData)eventData;

                if (questEventData.currentQuest.IsComplete && questEventData.currentQuest.Reward.RewardType == "XP")
                {
                    currentXP.value += questEventData.currentQuest.Reward.GetRewardValue();
                    Debug.Log($"you got {questEventData.currentQuest.Reward.GetRewardValue()} XP for completing {questEventData.currentQuest.name}");
                    CheckForLevelUP();
                }
            }
        }

        void CheckForLevelUP()
        {
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

        private void OnEnable()
        {
            EventBus<EnemyDieEventData>.OnEventPublished += OnEnemyKilled;
            EventBus<QuestEventData>.OnEventPublished += OnQuestComplete;
        }

        private void OnDisable()
        {
            EventBus<EnemyDieEventData>.OnEventPublished -= OnEnemyKilled;
            EventBus<QuestEventData>.OnEventPublished -= OnQuestComplete;
        }
    }
}
