using UnityEngine;

namespace JJN.QuestSystem.Quests
{
    [CreateAssetMenu(fileName = "KillEnemyQuest", menuName = "Quests/KillEnemyQuest")]
    public class KillEnemyQuest : Quest
    {
        [SerializeField]
        private string enemyToKill;
        [SerializeField]
        private int enemyCountToKill;
        private int _enemiesKilled;

        void IncreaseEnemiesKilled(EventData eventData)
        {
            EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;
            if (enemyDieEventData.enemyName == enemyToKill)
            {
                _enemiesKilled++;
                UpdateCurrentQuestText();
            }
        }

        public override void UpdateCurrentQuestText()
        {
            questText = $"{_enemiesKilled}/{enemyCountToKill} {enemyToKill}s killed";
            EventBus<QuestEventData>.Publish(new(this, false));
        }

        public override void Initialize()
        {
            _enemiesKilled = 0;
            IsComplete = false;
            questText = $"you need to kill {enemyCountToKill} {enemyToKill}s";
            EventBus<EnemyDieEventData>.OnEventPublished += IncreaseEnemiesKilled;
            
        }

        public override void Evaluate()
        {
            if (IsComplete) return;

            if (_enemiesKilled >= enemyCountToKill)
            {
                CompleteQuest();
                EventBus<EnemyDieEventData>.OnEventPublished -= IncreaseEnemiesKilled;
            }
        }
    }
}
