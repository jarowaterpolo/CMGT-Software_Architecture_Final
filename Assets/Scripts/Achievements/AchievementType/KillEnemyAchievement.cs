using NaughtyAttributes;
using UnityEngine;

namespace JJN.Achievements
{
    [CreateAssetMenu(fileName = "KillEnemyAchievement", menuName = "Achievements/KillEnemyAchievement")]
    public class KillEnemyAchievement : Achievement
    {
        [SerializeField]
        private bool specificEnemy;
        [SerializeField]
        private int amountToKill;

        [SerializeField, ShowIf("specificEnemy")]
        private string enemyToKill;

        private int _enemiesKilled;

        void IncreaseEnemiesKilled(EventData eventData)
        {
            EnemyDieEventData enemyDieEventData = (EnemyDieEventData)eventData;
            Debug.Log("achievement enemy killed");
            if (specificEnemy)
            {
                if (enemyDieEventData.enemyName == enemyToKill)
                {
                    _enemiesKilled++;
                    Debug.Log($"specific enemy killed, now killed {_enemiesKilled} of them");
                }
            }
            else
            {
                _enemiesKilled++;
                Debug.Log($"enemy killed, now killed {_enemiesKilled} of them");
            }
        }
        public override void Initialize()
        {
            base.Initialize();
            _enemiesKilled = 0;
            EventBus<EnemyDieEventData>.OnEventPublished += IncreaseEnemiesKilled;
        }
        public override void Evaluate()
        {
            if (achieved) return;

            if (_enemiesKilled >= amountToKill)
            {
                AchieveAchievement();
                EventBus<EnemyDieEventData>.OnEventPublished -= IncreaseEnemiesKilled;
            }
        }
    }
}
