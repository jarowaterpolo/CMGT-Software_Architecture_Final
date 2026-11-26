using UnityEngine;

namespace JJN.QuestSystem.Quests
{
    [CreateAssetMenu(fileName = "EnterGateQuest", menuName = "Quests/EnterGateQuest")]
    public class EnterGateQuest : Quest
    {
        private bool _gateEntered;
        public override void UpdateCurrentQuestText()
        {
            if (questText == QuestName) return;
            questText = QuestName;
        }
        public override void Register()
        {
            base.Register();
            _gateEntered = false;
            EventBus<AdvanceStageEventData>.OnEventPublished += GateEntered;
        }
        public override void Unregister()
        {
            EventBus<AdvanceStageEventData>.OnEventPublished -= GateEntered;
        }
        public override void Initialize()
        {
            if (!RegisterEarly)
            {
                Register();
            }
            UpdateCurrentQuestText();
        }

        public override void Evaluate()
        {
            if (IsComplete) return;

            if (_gateEntered)
            {
                CompleteQuest();
            }
        }

        void GateEntered(EventData eventData)
        {
            _gateEntered = true;
            Unregister();
        }

        public EnterGateQuest(string questName) 
        { 
            questText = questName;
        }
    }
}
