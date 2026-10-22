using JJN.QuestSystem.Quests;
using JJN.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace JJN.QuestSystem
{
    public class QuestController : MonoBehaviour
    {
        [SerializeField]
        private List<Quest> quests;
        private int _questIndex = 0;
        private Quest _currentQuest;
        private bool _isDead;

        void Start()
        {
            if (quests == null || quests.Count == 0) return;

            InitializeCurrentQuest();

        }

        void Update()
        {
            if (_currentQuest == null || _currentQuest.IsComplete || _isDead) return;

            _currentQuest.Evaluate();

            if (_currentQuest.IsComplete)
            {
                //Debug.Log($"Quest '{_currentQuest.QuestName}' completed!");
                EventBus<QuestEventData>.Publish(new(_currentQuest, true));
                AdvanceToNextQuest();
            }
            else
            {
                //EventBus<QuestEventData>.Publish(new(_currentQuest, false));
            }
        }

        void InitializeCurrentQuest()
        {
            if (_questIndex < quests.Count)
            {
                _currentQuest = quests[_questIndex];
                _currentQuest.Initialize();
                EventBus<QuestEventData>.Publish(new(_currentQuest, false));
                //Debug.Log($"started quest: {_currentQuest.QuestName}");
            }
            else
            {
                _currentQuest = null;
                DummyQuest allQuestDone = new("All quests completed");
                EventBus<QuestEventData>.Publish(new(allQuestDone, false));
                //Debug.Log("All quests Completed!");
            }
        }

        void AdvanceToNextQuest()
        {
            _questIndex++;
            InitializeCurrentQuest();
        }

        void SetIsDead(bool isDead)
        {
            _isDead = isDead;
            if (isDead)
            {
                DummyQuest playerDead = new("DIED");
                EventBus<QuestEventData>.Publish(new(playerDead, false));
                //Debug.Log("DIED!");
            }
        }

        private void OnEnable()
        {
            PlayerHP.onDeath += SetIsDead;
        }

        private void OnDisable()
        {
            PlayerHP.onDeath -= SetIsDead;
        }
    }
}
