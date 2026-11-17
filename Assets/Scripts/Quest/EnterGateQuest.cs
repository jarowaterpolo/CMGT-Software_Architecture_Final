using JJN.ItemSystem;
using UnityEngine;

namespace JJN.QuestSystem.Quests
{
    [CreateAssetMenu(fileName = "EnterGateQuest", menuName = "Quests/EnterGateQuest")]
    public class EnterGateQuest : Quest
    {
        public override void UpdateCurrentQuestText()
        {
            if (questText == QuestName) return;
            questText = QuestName;
        }
        public override void Initialize()
        {
            IsComplete = false;
            EventBus<AdvanceStageEventData>.OnEventPublished += GateEntered;

            UpdateCurrentQuestText();
        }

        public override void Evaluate()
        {
            if (IsComplete) return;
        }

        void GateEntered(EventData eventData)
        {
            IsComplete = true;
        }

        public EnterGateQuest(string questName) 
        { 
            questText = questName;
        }
    }
}
