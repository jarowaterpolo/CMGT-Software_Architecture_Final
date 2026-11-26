using UnityEngine;

namespace JJN.Achievements
{
    public class PopUpAchievementService : MonoBehaviour, IAchievementService
    {
        [SerializeField]
        private AchievementUIController achievementUIController;
        public void DisplayAchievement(Achievement achievement)
        {
            achievementUIController.SetUI(achievement);
            var achievementStr = $"{achievement.name} reached by {achievement.description} \n";
            Debug.Log(achievementStr);
            achievement.hasBeenDisplayed = true;
        }
    }
}