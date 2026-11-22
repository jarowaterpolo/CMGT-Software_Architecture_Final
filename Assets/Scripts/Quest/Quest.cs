using JJN.QuestSystem.Rewards;
using UnityEngine;

namespace JJN.QuestSystem.Quests
{
    public abstract class Quest : ScriptableObject
    {
        [SerializeField] private string questName;
        public string QuestName => questName;
        [SerializeField] private Reward reward;
        public Reward Reward => reward;
        [SerializeField] private bool registerEarly;
        public bool RegisterEarly => registerEarly;

        [HideInInspector]
        public string questText;
        public bool IsComplete {get; protected set;}
        public virtual void Register()
        {
            IsComplete = false;
        }
        public virtual void Unregister() { }
        public abstract void Initialize();
        public abstract void Evaluate();
        public abstract void UpdateCurrentQuestText();
        public void CompleteQuest()
        {
            IsComplete = true;
        }
    }
}
