using TMPro;
using UnityEngine;

namespace JJN.FSM
{
    public class AlignToState : State
    {
        private Vector3 targetPos;
        private Transform transform;
        private Vector3 direction;
        private float rotationSign;
        private bool aligningToMovingTarget;

        public AlignToState(Transform pTransform, Blackboard pBlackboard, bool pAligningToMovingTarget = false) 
        { 
            transform = pTransform;
            blackboard = pBlackboard;
            aligningToMovingTarget = pAligningToMovingTarget;
        }

        // Called when entering the state. Sets the initial target position.
        public override void Enter()
        {
            base.Enter();

            Debug.Log($"just started using align state looking to {blackboard.targetPosition}");

            // Determine the target position (static or moving) and set it
            if (aligningToMovingTarget)
            {
                SetTarget(blackboard.targetTransform.position);
            }
            else
            {
                SetTarget(blackboard.targetPosition);
            }
        }

        // Sets the target position and calculates the direction and rotation sign.
        public void SetTarget(Vector3 pTargetPosition)
        {
            targetPos = pTargetPosition;

            // Compute the normalized direction vector toward the target
            direction = (targetPos - transform.position).normalized;

            // Determine whether to rotate clockwise or counterclockwise
            rotationSign = Mathf.Sign(Vector3.Dot(transform.right, direction));
        }

        // Called every frame while the state is active. Rotates the object toward the target.
        public override void Step()
        {
            base.Step();

            // If the target is moving, update its position
            if (aligningToMovingTarget)
            {
                SetTarget(blackboard.targetTransform.position);
            }

            // Rotate around the up axis based on direction and speed
            transform.Rotate(transform.up, rotationSign * blackboard.rotateSpeed * Time.deltaTime);
        }

        // Checks if the object is aligned within a threshold (dot product) toward the target.
        // Returns true if aligned within acceptable margin, otherwise false.
        public bool AlignedWithTarget()
        {
            // Dot product close to 1 means the object is almost facing the target direction
            return Vector3.Dot(transform.forward, direction) >= 0.999f;
        }
    }
}
