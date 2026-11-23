using JJN.PlayerSystem.Inventory;
using UnityEngine;

namespace JJN.FSM
{
    public class BossEnemyFSM : FSM
    {
        public PatrollFSM patrolFSM;
        public BossSeekAndAttackFSM bossSeekAndAttackFSM;

        private Transform _transform;
        
        public BossEnemyFSM(Transform transform, Blackboard pBlackboard)
        {
            //Debug.Log($"enemy FSM blackboard = {pBlackboard}");
            blackboard = pBlackboard;

            patrolFSM = new PatrollFSM(transform, blackboard);
            bossSeekAndAttackFSM = new BossSeekAndAttackFSM(transform, blackboard);
            _transform = transform;

            patrolFSM.transitions.Add(new(TargetInChaseRange, bossSeekAndAttackFSM));
            bossSeekAndAttackFSM.transitions.Add(new(TargetOutChaseRange, patrolFSM));


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
