using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace PlayerSystem.Movement
{
    public class PatrollAndChaseWhenInRange : MoveBehaviour
    {
        [SerializeField]
        private bool doDebugDraw;

        [SerializeField]
        private float patrollChangeTargetDelay = 2f;
        [SerializeField]
        private Vector2 patrollRange = new(5,5);

        private Vector3 startPos;
        private Vector3 patrolTagetPos;

        private bool targetInRange;
        private Vector3 targetPosition;

        private void Start()
        {
            startPos = transform.position;
            StartCoroutine(SetPartolTarget());
        }
        private void Update()
        {
            targetInRange = Vector3.Distance(transform.position, targetPosition) < targetRange;
        }
        public void FixedUpdate()
        {
            if (targetInRange) 
            {
                transform.LookAt(targetPosition);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolTagetPos, Time.deltaTime);
            }
        }
        private IEnumerator SetPartolTarget()
        {
            yield return new WaitForSeconds(patrollChangeTargetDelay);
            patrolTagetPos = new(startPos.x + Random.Range(0, patrollRange.x), startPos.y, startPos.z + Random.Range(0, patrollRange.y));
        }
        public override void SetTargetPosition(Vector3 targetPos)
        {
            targetPosition = targetPos;
        }

        private void OnDrawGizmos()
        {
            if (!doDebugDraw) return;
            Gizmos.color = new Color(0f, 0f, 1f, .2f); 

            // Draw the sphere.
            Gizmos.DrawSphere(transform.position, targetRange);

            // Draw wire sphere outline.
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, targetRange);
        }
    }
}
