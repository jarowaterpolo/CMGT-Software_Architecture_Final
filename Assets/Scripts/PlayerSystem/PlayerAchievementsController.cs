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
                    achievementService.DisplayAchievement(achievement);
                }
                else
                {
                    Match match = Regex.Match(achievement.description, @"-?\d+");

                    if (match.Success && int.TryParse(match.Value, out int achievementValueToReach))
                    {                       
                        if ( >= achievementValueToReach)
                        {
                            achievement.achieved = true;
                        }
                    }
                }
            }
        }
    }
}
