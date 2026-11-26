using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace JJN.Achievements
{
    public class PlayerAchievementsController : MonoBehaviour
    {
        private IAchievementService achievementService;

        [SerializeField]
        private Achievement[] achievements;

        private void Start()
        {
            achievementService = ServiceLocator.GetService<IAchievementService>();
            if (achievementService == null)
            {
                throw new Exception("No achievement display service found.");
            }

            foreach (Achievement achievement in achievements)
            {
                achievement.Initialize();
                achievement.hasBeenDisplayed = false;
            }
        }

        private void Update()
        {
            ProcessAchievements();
        }

        private void ProcessAchievements()
        {
            foreach (Achievement achievement in achievements)
            {
                if (achievement.achieved)
                {
                    if (!achievement.hasBeenDisplayed)
                    {
                        achievementService.DisplayAchievement(achievement);
                    }
                }
                else
                {
                    achievement.Evaluate();
                }
            }
        }
    }
}
