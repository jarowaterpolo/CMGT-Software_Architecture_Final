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

        public static DummyQuest Create(string questName)
        {
            DummyQuest quest = CreateInstance<DummyQuest>();
            quest.questText = questName;
            return quest;
        }
    }
}
