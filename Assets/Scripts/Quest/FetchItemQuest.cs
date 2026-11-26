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
        public override void Register()
        {
            base.Register();
            itemFetched = false;
            DroppedItem.onPickupItem += CheckItem;
            UpdateCurrentQuestText();
        }
        public override void Unregister()
        {
            DroppedItem.onPickupItem -= CheckItem;
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

            if (itemFetched)
            {
                CompleteQuest();
                Unregister();
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
