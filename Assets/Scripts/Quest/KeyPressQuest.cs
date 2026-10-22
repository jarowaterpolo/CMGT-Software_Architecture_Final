using UnityEngine;

namespace JJN.QuestSystem.Quests
{
    [CreateAssetMenu(fileName = "KeyPressQuest", menuName = "Quests/KeyPressQuest")]
    public class KeyPressQuest : Quest
    {
        [SerializeField]
        private KeyCode[] keysToPress;
        private bool[] keysPressedState;

        public override void UpdateCurrentQuestText()
        {
            if (questText == QuestName) return;
            questText = QuestName;
        }
        public override void Initialize()
        {
            IsComplete = false;

            UpdateCurrentQuestText();

            if (keysToPress != null)
            {
                keysPressedState = new bool[keysToPress.Length];
            }
        }

        public override void Evaluate()
        {
            if (IsComplete || keysToPress == null || keysToPress.Length == 0) return;

            for (int i = 0; i < keysToPress.Length; i++)
            {
                if (Input.GetKeyDown(keysToPress[i]))
                {
                    keysPressedState[i] = true;
                }
            }

            bool allKeysPressed = true;
            for (int i = 0; i < keysPressedState.Length; i++) 
            { 
                if (!keysPressedState[i])
                {
                    allKeysPressed = false;
                    break;
                }
            }

            if (allKeysPressed)
            {
                CompleteQuest();
            }
        }
    }
}
