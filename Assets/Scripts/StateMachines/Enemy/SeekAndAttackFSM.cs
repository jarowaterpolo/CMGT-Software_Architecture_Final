using UnityEngine;

namespace JJN.FSM
{
    public class SeekAndAttackFSM : FSM
    {
        private AlignToState alignToState;
        private IdleState idleState;
        private MoveToState moveToState;

        public SeekAndAttackFSM(Transform transform, Blackboard pBlackboard)
        {
            blackboard = pBlackboard;

            alignToState = new(transform, blackboard, true);
            idleState = new(blackboard);
            moveToState = new(transform, blackboard, true);

            idleState.transitions.Add(new(idleState.IdleTimeReached, alignToState));

            alignToState.transitions.Add(new(alignToState.AlignedWithTarget, moveToState));

            moveToState.transitions.Add(new(moveToState.TargetReached, idleState));
            moveToState.transitions.Add(new(alignToState.NotAlignedWithTarget, alignToState));
        }

        public override void Enter()
        { 
            blackboard.targetPosition = blackboard.targetTransform.position;
            base.Enter();
            currentState = idleState;
            currentState.Enter();
        }
    }
}
