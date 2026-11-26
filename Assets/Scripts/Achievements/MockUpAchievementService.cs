using UnityEngine;

namespace JJN.Achievements
{
    public class MockUpAchievementService : MonoBehaviour, IAchievementService
    {
        public void DisplayAchievement(Achievement achievement)
        {
            var achievementStr = $"{achievement.name} reached by {achievement.description} \n";
            Debug.Log(achievementStr);
        }
    }
}