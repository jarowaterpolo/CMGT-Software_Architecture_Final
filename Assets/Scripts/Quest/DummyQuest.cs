using UnityEngine;

namespace JJN.QuestSystem.Quests
{
    public class DummyQuest : Quest
    {
        public override void UpdateCurrentQuestText()
        {

        }
        public override void Initialize()
        {

        }

        public override void Evaluate()
        {

        }

        public DummyQuest(string questName) 
        { 
            questText = questName;
        }
    }
}
