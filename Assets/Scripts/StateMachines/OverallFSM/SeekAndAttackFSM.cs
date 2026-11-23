using UnityEngine;

namespace JJN.FSM
{
    public class SeekAndAttackFSM : FSM
    {
        private AlignToState alignToState;
        private MoveToState moveToState;
        private AttackState attackState;

        private Transform _transform;

        public SeekAndAttackFSM(Transform transform, Blackboard pBlackboard)
        {
            blackboard = pBlackboard;

            _transform = transform;

            alignToState = new(transform, blackboard, true);
            moveToState = new(transform, blackboard, true);
            attackState = new(transform, blackboard);

            alignToState.transitions.Add(new(() => { return AttackTargetFound() && alignToState.AlignedWithTarget(); }, attackState));
            alignToState.transitions.Add(new(() => { return AttackTargetLost() && alignToState.AlignedWithTarget(); }, moveToState));

            moveToState.transitions.Add(new(AttackTargetFound, alignToState));
            moveToState.transitions.Add(new(alignToState.NotAlignedWithTarget, alignToState));

            attackState.transitions.Add(new(attackState.FinishedAttacking, alignToState));
        }

        public override void Enter()
        { 
            blackboard.targetPosition = blackboard.targetTransform.position;
            base.Enter();
            currentState = alignToState;
            currentState.Enter();
        }

        // Returns true if the player is close enough to attack and is visible.
        private bool AttackTargetFound()
        {
            return Vector3.Distance(_transform.position, blackboard.targetTransform.position) <= blackboard.weapon.Range;
        }

        // Returns true if the player moved out of attack range or became hidden.
        private bool AttackTargetLost()
        {
            return !AttackTargetFound();
        }
    }
}
