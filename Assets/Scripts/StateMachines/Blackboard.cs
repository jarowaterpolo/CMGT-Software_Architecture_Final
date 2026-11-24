using JJN.ItemSystem;
using System.Collections.Generic;
using UnityEngine;

namespace JJN.FSM
{
    public class Blackboard : MonoBehaviour
    {
        [HideInInspector] public Transform targetTransform;
        [HideInInspector] public Vector3 targetPosition;

        public float chaseRange = 5.0f;
        [HideInInspector] public Weapon weapon;

        [HideInInspector] public float attackDelay = 1.0f;

        public float waitingTime = 2.0f;
        public float distanceThreshold = .2f;
        public float moveSpeed = 10.0f;
        public float rotateSpeed = 90.0f;

        [HideInInspector] public List<Vector3> waypoints;
        [HideInInspector] public int waypointIndex = 0;

        [HideInInspector] public int maxHP;
        [HideInInspector] public int hp;

        public bool doDebugDraw;
    }
}
