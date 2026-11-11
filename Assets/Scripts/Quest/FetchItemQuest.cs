using JJN.ItemSystem;
using UnityEngine;

namespace JJN.QuestSystem.Quests
{
    [CreateAssetMenu(fileName = "FetchItemQuest", menuName = "Quests/FetchItemQuest")]
    public class FetchItemQuest : Quest
    {
        [SerializeField]
        private ItemData itemToFetch;
        private bool itemFetched;
        public override void UpdateCurrentQuestText()
        {
            if (questText == QuestName) return;
            questText = QuestName;
        }
        public override void Initialize()
        {
            IsComplete = false;
            DroppedItem.onPickupItem += CheckItem;

            UpdateCurrentQuestText();
        }

        public override void Evaluate()
        {
            if (IsComplete) return;

            if (itemFetched)
            {
                CompleteQuest();
                DroppedItem.onPickupItem -= CheckItem;
            }
        }

        void CheckItem(Item item)
        {
            if (itemToFetch.id == item.Id)
            {
                itemFetched = true;
            }
        }

        public FetchItemQuest(string questName) 
        { 
            questText = questName;
        }
    }
}
