using JJN.ItemSystem;
using System.Collections.Generic;
using UnityEngine;

namespace JJN.FSM
{
    public class Blackboard : MonoBehaviour
    {
        public Transform targetTransform;
        public Vector3 targetPosition;

        public float chaseRange = 5.0f;
        public Weapon weapon;
        public float attackDelay = 1.0f;

        public float waitingTime = 2.0f;
        public float distanceThreshold = .2f;
        public float moveSpeed = 10.0f;
        public float rotateSpeed = 90.0f;

        public List<Vector3> waypoints;
        public int waypointIndex = 0;

        public bool doDebugDraw;
    }
}
