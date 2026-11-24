using JJN.FSM;
using JJN.PlayerSystem.Values;
using JJN.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PlayerSystem.Values
{
    public class PlayerXPController : MonoBehaviour
    {
        public int[] xpNeededForEachLevel; 
        public IntValueWithAction currentXP; 
        public IntValueWithAction currentLevel;

        public delegate void LVLHandler(int currentLVL, int MaxLvlCount);
        public static event LVLHandler OnLevelUp;

        public delegate void XPHandler(int currentXP, int MaxXP);
        public static event XPHandler OnXPChange;

        public void OnEnemyKilled(EventData eventData)
        {
            if (currentLevel.value <= xpNeededForEachLevel.Length)
            {
                EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;
                GetXP(enemyDieEventData.enemy.enemyData.XP);
            }
        }
        public void OnQuestComplete(EventData eventData)
        {
            if (currentLevel.value <= xpNeededForEachLevel.Length)
            {
                QuestEventData questEventData = (QuestEventData)eventData;

                if (questEventData.currentQuest.IsComplete && questEventData.currentQuest.Reward.RewardType == "XP")
                {
                    GetXP(questEventData.currentQuest.Reward.GetRewardValue());
                }
            }
        }

        void CheckForLevelUP()
        {
            if (currentLevel.value > xpNeededForEachLevel.Length) return;
            int previousLevel = currentLevel.value;

            while (currentXP.value >= xpNeededForEachLevel[currentLevel.value - 1])
            {
                currentXP.value -= xpNeededForEachLevel[currentLevel.value - 1];

                currentLevel.value++;
                OnLevelUp?.Invoke(currentLevel.value, xpNeededForEachLevel.Length);

                if (currentLevel.value > xpNeededForEachLevel.Length)
                {
                    if (currentLevel.value > xpNeededForEachLevel.Length)
                    {
                        OnXPChange?.Invoke(1, 1);
                        break;
                    }
                }
                else
                {
                    OnXPChange?.Invoke(currentXP.value, xpNeededForEachLevel[currentLevel.value - 1]);
                }
            }
        }

        void GetXP(int value)
        {
            if (currentLevel.value > xpNeededForEachLevel.Length) return;
            currentXP.value += value;
            OnXPChange?.Invoke(currentXP.value, xpNeededForEachLevel[currentLevel.value - 1]);
            CheckForLevelUP();
        }

        void ResetXPAndLevel()
        {
            currentXP.ResetValue();
            currentLevel.ResetValue();
            OnXPChange?.Invoke(currentXP.value, xpNeededForEachLevel[currentLevel.value - 1]);
            OnLevelUp?.Invoke(currentLevel.value, xpNeededForEachLevel.Length);
        }

        private void OnEnable()
        {
            OnXPChange?.Invoke(currentXP.value, xpNeededForEachLevel[currentLevel.value - 1]);
            EventBus<EnemyDieEventData>.OnEventPublished += OnEnemyKilled;
            EventBus<QuestEventData>.OnEventPublished += OnQuestComplete;
            SceneManager.sceneLoaded += OnSceneLoaded;
            PlayerDeathController.OnPlayerReset += ResetXPAndLevel;
        }

        private void OnDisable()
        {
            EventBus<EnemyDieEventData>.OnEventPublished -= OnEnemyKilled;
            EventBus<QuestEventData>.OnEventPublished -= OnQuestComplete;
            SceneManager.sceneLoaded -= OnSceneLoaded;
            PlayerDeathController.OnPlayerReset -= ResetXPAndLevel;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            OnXPChange?.Invoke(currentXP.value, xpNeededForEachLevel[currentLevel.value - 1]);
            OnLevelUp?.Invoke(currentLevel.value, xpNeededForEachLevel.Length);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                GetXP(50);
            }
        }
#endif
    }
}
