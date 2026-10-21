using UnityEngine;

namespace JJN.QuestSystem.Quests
{
    [CreateAssetMenu(fileName = "NewMovementQuest", menuName = "Quests/MovementQuest")]
    public class MovementQuest : Quest
    {
        private bool w, a, s, d;

        public override void Initialize()
        {
            IsComplete = false;
            w = a = s = d = false;
        }

        public override void Evaluate()
        {
            if (IsComplete) return;

            if (Input.GetKeyDown(KeyCode.W)) w = true;
            if (Input.GetKeyDown(KeyCode.A)) a = true;
            if (Input.GetKeyDown(KeyCode.S)) s = true;
            if (Input.GetKeyDown(KeyCode.D)) d = true;

            if (w && a && s && d)
            {
                CompleteQuest();
            }
        }
    }
}
