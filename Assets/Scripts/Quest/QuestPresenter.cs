using TMPro;
using UnityEngine;

namespace JJN.QuestSystem
{
    public class QuestPresenter : MonoBehaviour
    {
        private TMP_Text questText;
        private string currentQuestText;

        void Start()
        {
            questText = GetComponent<TMP_Text>();
        }

        void SetQuestText(EventData eventData)
        {
            QuestEventData questEventData = (QuestEventData)eventData;

            if (questEventData.currentQuestText != currentQuestText)
            {
                currentQuestText = questEventData.currentQuestText;
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
