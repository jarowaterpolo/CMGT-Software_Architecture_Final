using UnityEngine;

namespace JJN.FSM
{
    public class IdleState : State
    {
        private float startTime;
        public IdleState(Blackboard pBlackboard)
        {
            blackboard = pBlackboard;
        }
        public override void Enter()
        {
            base.Enter();
            startTime = Time.time;
        }
        public bool IdleTimeReached()
        {
            return Time.time > startTime + blackboard.waitingTime;
        }
    }
}
