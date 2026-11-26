    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

namespace JJN.Achievements
{
    public class AchievementUIController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text idText;
        [SerializeField]
        private TMP_Text nameText;
        [SerializeField]
        private TMP_Text descriptionText;
        [SerializeField]
        private Image icon;

        public void SetUI(Achievement achievement)
        {
            idText.text = achievement.id.ToString();
            nameText.text = achievement.name;
            descriptionText.text = achievement.description;
            icon.sprite = achievement.sprite;

            // Update the UI layout before scrolling.
            Canvas.ForceUpdateCanvases();
        }
    }

}
