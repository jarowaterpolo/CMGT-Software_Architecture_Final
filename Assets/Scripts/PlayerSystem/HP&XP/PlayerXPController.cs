using JJN.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerSystem.Values
{
    public class PlayerXPController : MonoBehaviour
    {
        public event Action<int> onLevelUp;

        public int[] xpNeededForEachLevel; // An array to set needed XP to level up for each level

        private int currentXP = 0; // Current accumulated experience points

        private int currentLevel = 1; // Current hero level, starts at level 1

        [Header("XP Bar display")]
        [SerializeField]
        private Image xpBar; // UI progress bar showing current XP progress
        [SerializeField]
        private TMP_Text lvlText;
        [SerializeField]
        private int MaxHPGainBasedOnLevelUp;
        [SerializeField]
        private HP_ScriptableObject hpObject;

        private void Start()
        {
            // Initialize XP bar to empty
            xpBar.fillAmount = 0f;
        }

        public void OnEnemyKilled(EventData eventData)
        {
            // Only process XP if the hero hasn't reached the final level
            if (currentLevel <= xpNeededForEachLevel.Length)
            {
                // Cast event data to EnemyDieEventData to access XP reward
                EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;

                // Add enemy's XP value to hero's current XP
                currentXP += enemyDieEventData.enemy.XP;

                int previousLevel = currentLevel;

                // Check for multiple level-ups if XP is enough
                while (currentXP >= xpNeededForEachLevel[currentLevel - 1])
                {
                    // Increase player's values according to level-up bonuses


                    // Subtract XP needed for this level
                    currentXP -= xpNeededForEachLevel[currentLevel - 1];

                    // Increase hero's level
                    currentLevel++;

                    // Break if max level is reached
                    if (currentLevel > xpNeededForEachLevel.Length)
                    {
                        break;
                    }
                }

                // If hero reached max level, fill XP bar completely
                if (currentLevel == xpNeededForEachLevel.Length + 1)
                {
                    xpBar.fillAmount = 1f;
                }
                else
                {
                    // Otherwise, update XP bar based on progress toward next level
                    xpBar.fillAmount = (float)currentXP / (float)xpNeededForEachLevel[currentLevel - 1];
                }

                // Trigger level-up event if level increased
                if (currentLevel > previousLevel)
                {
                    onLevelUp?.Invoke(currentLevel);
                }
            }
        }

        public void SetLvlText(int lvl)
        {
            if (lvl > xpNeededForEachLevel.Length)
            {
                lvlText.text = $"MaxLvl";
            }
            else
            {
                lvlText.text = $"Lvl:{lvl}";
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
            onLevelUp += SetLvlText;
            onLevelUp += GainMaxHPOnLevelUp;
            hpObject.onMaxHPValueChanged += GainHPOnLevelUP;
            EventBus<EnemyDieEventData>.OnEventPublished += OnEnemyKilled;
        }

        private void OnDisable()
        {
            onLevelUp -= SetLvlText;
            onLevelUp -= GainMaxHPOnLevelUp;
            hpObject.onMaxHPValueChanged -= GainHPOnLevelUP;
            EventBus<EnemyDieEventData>.OnEventPublished -= OnEnemyKilled;
        }
    }
}
