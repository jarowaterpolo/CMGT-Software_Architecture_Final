using UnityEngine;
using UnityEngine.AI;

namespace JJN.FSM
{
    public class MoveToState : State
    {
        private Transform _transform;
        private bool followMovingTarget;
        private float speed;

        public MoveToState(Transform transform, Blackboard pBlackboard, bool pFollowMovingTarget = false)
        {
            _transform = transform;
            blackboard = pBlackboard;
            followMovingTarget = pFollowMovingTarget;
        }

        public override void Enter()
        {
            base.Enter();
            speed = blackboard.moveSpeed;

            Debug.Log($"just started using move state moving to {blackboard.targetPosition}");
        }

        // Called every frame to update movement toward the target.
        // The destination is updated only if the target is moving.
        public override void Step()
        {
            base.Step();

            Vector3 targetPos;
            if (!followMovingTarget)
            {
                targetPos = blackboard.targetPosition;
            }
            else
            {
                targetPos = blackboard.targetTransform.position;
            }

            _transform.position = Vector3.MoveTowards(_transform.position, targetPos, blackboard.moveSpeed * Time.deltaTime);
        }

        // Checks whether the agent has reached the target (static or moving).
        // Uses the blackboard's distance threshold to determine proximity.
        public bool TargetReached()
        {
            if (!followMovingTarget)
            {
                return Vector3.Distance(_transform.position, blackboard.targetPosition) < blackboard.distanceThreshold;
            }
            else
            {
                return Vector3.Distance(_transform.position, blackboard.targetTransform.position) < blackboard.distanceThreshold;
            }
        }
    }
}
