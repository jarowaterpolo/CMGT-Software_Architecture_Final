using UnityEngine;

namespace PlayerSystem.Movement
{
    public abstract class MoveBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected float targetRange = .2f;

        public abstract void SetTargetPosition(Vector3 targetPos);
    }
}
