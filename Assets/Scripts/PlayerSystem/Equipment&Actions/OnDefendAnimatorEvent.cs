using UnityEngine;

namespace PlayerSystem.Animation
{
    public class OnDefendAnimatorEvent : MonoBehaviour
    {
        [SerializeField]
        private string parameterName = "IsDefending";

        private Animator _animator;
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        void PlayDefendAnimation(EventData eventData)
        {
            DefendEventData defendEventData = (DefendEventData)eventData;
            if (defendEventData.shieldUser.CompareTag("Player"))
            {
                _animator.SetBool(parameterName, defendEventData.isBlocking);
            }
        }
        private void OnEnable()
        {
            EventBus<DefendEventData>.OnEventPublished += PlayDefendAnimation;
        }

        private void OnDisable()
        {
            EventBus<DefendEventData>.OnEventPublished -= PlayDefendAnimation;
        }
    }
}
