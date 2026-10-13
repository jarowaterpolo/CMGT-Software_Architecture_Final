using UnityEngine;

namespace PlayerSystem.Animation
{
    public class OnAttackAnimatorEvent : MonoBehaviour
    {
        [SerializeField]
        private string stateName = "Attack";
        private Animator _animator;
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        void PlayAttackAnimation(EventData eventData)
        {
            AttackEventData attackEventData = (AttackEventData)eventData;
            if (attackEventData.weaponUser.CompareTag("Player"))
            {
                _animator.Play(stateName);
            }
        }
        private void OnEnable()
        {
            EventBus<AttackEventData>.OnEventPublished += PlayAttackAnimation;
        }

        private void OnDisable()
        {
            EventBus<AttackEventData>.OnEventPublished -= PlayAttackAnimation;
        }
    }
}
