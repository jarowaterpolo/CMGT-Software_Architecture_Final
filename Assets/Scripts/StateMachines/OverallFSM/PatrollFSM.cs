using UnityEngine;

namespace JJN.FSM
{
    public class PatrollFSM : FSM
    {
        private AlignToState alignToState;
        private IdleState idleState;
        private MoveToState moveToState;

        public PatrollFSM(Transform transform, Blackboard pBlackboard)
        {
            blackboard = pBlackboard;

            alignToState = new(transform, blackboard);
            idleState = new(blackboard);
            moveToState = new(transform, blackboard);

            idleState.transitions.Add(new(idleState.IdleTimeReached, alignToState));
            alignToState.transitions.Add(new(alignToState.AlignedWithTarget, moveToState));
            moveToState.transitions.Add(new(moveToState.TargetReached, idleState));

            moveToState.onExit += StopMoving;
        }

        public override void Enter()
        {
            blackboard.targetPosition = blackboard.waypoints[0];
            base.Enter();
            currentState = idleState;
            currentState.Enter();
        }

        /// <summary>
        /// Called after finishing movement.
        /// Advances the waypoint index and updates the blackboard with new target and wait time.
        /// </summary>
        public void StopMoving()
        {
            // Loop through the waypoint array
            if (blackboard.waypointIndex < blackboard.waypoints.Count - 1)
            {
                blackboard.waypointIndex++;
            }
            else
            {
                blackboard.waypointIndex = 0;
            }

            // Update blackboard with new target and wait time
            blackboard.targetPosition = blackboard.waypoints[blackboard.waypointIndex];
        }
    }
}
