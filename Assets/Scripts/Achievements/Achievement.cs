using UnityEngine;
using UnityEngine.UI;

namespace JJN.Achievements
{
    public abstract class Achievement : ScriptableObject
    {
        public int id;
        public string achievementName;
        public string description;
        public bool achieved { get; protected set; }
        public Sprite sprite;
        [HideInInspector]
        public bool hasBeenDisplayed = false;

        public virtual void Initialize()
        {
            achieved = false;
        }
        public abstract void Evaluate();
        public void AchieveAchievement()
        {
            achieved = true;
        }
    }
}