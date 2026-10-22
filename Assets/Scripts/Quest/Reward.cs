using UnityEngine;

namespace JJN.QuestSystem.Rewards
{
    [CreateAssetMenu(fileName = "Reward", menuName = "Rewards/Reward")]
    public class Reward : ScriptableObject
    {
        [SerializeField] private string rewardType;
        public string RewardType => rewardType;
        [SerializeField] private int rewardValue;

        public int GetRewardValue()
        {
            return rewardValue;
        }
    }
}
