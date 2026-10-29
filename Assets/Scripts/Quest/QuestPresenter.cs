using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace JJN.QuestSystem
{
    public class QuestPresenter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text questText;
        private string currentQuestText;

        private void Awake()
        {
            if (questText != null) return;
            questText = GetComponent<TMP_Text>();            
        }

        void SetQuestText(EventData eventData)
        {
            QuestEventData questEventData = (QuestEventData)eventData;

            if (questEventData.currentQuest.questText != currentQuestText)
            {
                //Debug.Log($"event quest text = {questEventData.currentQuest.questText}");
                currentQuestText = questEventData.currentQuest.questText;
                //Debug.Log($"current quest is: {currentQuestText}");
                questText.text = currentQuestText;
            }
        }

        private void OnEnable()
        {
            EventBus<QuestEventData>.OnEventPublished += SetQuestText;
        }

        private void OnDisable()
        {
            EventBus<QuestEventData>.OnEventPublished -= SetQuestText;
        }
    }
}
