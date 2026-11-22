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
            }
        }

        public override void UpdateCurrentQuestText()
        {
            questText = $"{_enemiesKilled}/{enemyCountToKill} {enemyToKill}s killed";
            EventBus<QuestEventData>.Publish(new(this, false));
        }

        public override void Register()
        {
            base.Register();
            questText = $"you need to kill {enemyCountToKill} {enemyToKill}s";
            _enemiesKilled = 0;
            EventBus<EnemyDieEventData>.OnEventPublished += IncreaseEnemiesKilled;   
        }

        public override void Unregister()
        {
            EventBus<EnemyDieEventData>.OnEventPublished -= IncreaseEnemiesKilled;
        }

        public override void Initialize()
        {
            if (!RegisterEarly)
            {
                Register();
            }
        }

        public override void Evaluate()
        {
            if (IsComplete) return;

            UpdateCurrentQuestText();

            if (_enemiesKilled >= enemyCountToKill)
            {
                CompleteQuest();
                Unregister();
            }
        }
    }
}
