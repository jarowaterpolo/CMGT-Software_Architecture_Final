using JJN.QuestSystem.Quests;
using System.Collections.Generic;
using UnityEngine;

namespace JJN.QuestSystem
{
    public class QuestController : MonoBehaviour
    {
        [SerializeField]
        private List<Quest> quests;
        private int questIndex = 0;
        private Quest currentQuest;

        void Start()
        {
            if (quests == null || quests.Count == 0) return;

            InitializeCurrentQuest();

        }

        void Update()
        {
            if (currentQuest == null || currentQuest.IsComplete) return;

            currentQuest.Evaluate();

            if (currentQuest.IsComplete)
            {
                Debug.Log($"Quest '{currentQuest.QuestName}' completed!");
                EventBus<QuestEventData>.Publish(new(currentQuest.QuestName, true));

                AdvanceToNextQuest();
            }
        }

        void InitializeCurrentQuest()
        {
            if (questIndex < quests.Count)
            {
                currentQuest = quests[questIndex];
                currentQuest.Initialize();
                EventBus<QuestEventData>.Publish(new(currentQuest.QuestName, false));
                Debug.Log($"started quest: {currentQuest.QuestName}");
            }
            else
            {
                currentQuest = null;
                Debug.Log("All quests Completed!");
            }
        }

        void AdvanceToNextQuest()
        {
            questIndex++;
            InitializeCurrentQuest();
        }
    }
}
