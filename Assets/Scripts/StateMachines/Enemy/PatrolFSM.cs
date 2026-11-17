using UnityEngine;

namespace JJN.FSM
{
    public class PatrolFSM : FSM
    {
        public PatrolFSM(Transform pTransform, Blackboard pBlackboard)
        {
            blackboard = pBlackboard;
        }
    }
}
