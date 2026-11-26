using System;
using UnityEngine;
using UnityEngine.UI;

//[Serializable]
public abstract class Achievement : ScriptableObject
{
    public int id;
    public string achievementName;
    public string description;
    public bool achieved;
    public Image image;

    public bool MilestoneReached { get; protected set; }
    public virtual void Register()
    {
        MilestoneReached = false;
    }
    public virtual void Unregister() { }
    public abstract void Initialize();
    public abstract void Evaluate();
    public abstract void UpdateCurrentQuestText();
    public void AchievementAchieved()
    {
        MilestoneReached = true;
    }
}
