using UnityEngine;

namespace PlayerSystem.Movement
{
    public abstract class MoveBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected float targetRange = .2f;
        [SerializeField]
        protected float Speed = 10f;
        protected bool isMoving = false;
        public abstract void SetTargetPosition(Vector3 targetPos);
    }
}
