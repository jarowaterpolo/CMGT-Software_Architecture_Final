using JJN.PlayerSystem.Inventory;
using UnityEngine;

namespace JJN.FSM
{
    public class EnemyFSM : FSM
    {
        public PatrollFSM patrolFSM;
        public SeekAndAttackFSM seekAndAttackFSM;

        private Transform _transform;
        
        public EnemyFSM(Transform transform, Blackboard pBlackboard)
        {
            //Debug.Log($"enemy FSM blackboard = {pBlackboard}");
            blackboard = pBlackboard;

            patrolFSM = new PatrollFSM(transform, blackboard);
            seekAndAttackFSM = new SeekAndAttackFSM(transform, blackboard);
            _transform = transform;

            patrolFSM.transitions.Add(new(TargetInChaseRange, seekAndAttackFSM));
            seekAndAttackFSM.transitions.Add(new(TargetOutChaseRange, patrolFSM));


            currentState = patrolFSM;
        }

        bool TargetInChaseRange()
        {
            return Vector3.Distance(_transform.position, blackboard.targetTransform.position) <= blackboard.chaseRange;
        }

        bool TargetOutChaseRange()
        {
            return Vector3.Distance(_transform.position, blackboard.targetTransform.position) > blackboard.chaseRange;
        }

        // Called when this FSM becomes active. Triggers the Enter method on the current sub-FSM.
        public override void Enter()
        {
            base.Enter();
            currentState.Enter();
        }
    }
}
