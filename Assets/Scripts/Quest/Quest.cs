using UnityEngine;

namespace JJN.QuestSystem.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
    public abstract class Quest : ScriptableObject
    {
        [SerializeField] private string questName;

        public string QuestName => questName;
        public bool IsComplete {get; protected set;}
        public abstract void Initialize();
        public abstract void Evaluate();

        public void CompleteQuest()
        {
            IsComplete = true;
        }
    }
}
