using UnityEngine;

namespace PlayerSystem.Animation
{
    public class OnAttackAnimatorEvent : MonoBehaviour
    {
        [SerializeField]
        private string stateName = "Attack";
        private GameObject caster = null;
        private Animator _animator;
        private void Start()
        {
            _animator = GetComponent<Animator>();

            Transform current = transform;
            while (current != null)
            {
                if (current.CompareTag("Player") || current.CompareTag("Enemy"))
                {
                    caster = current.gameObject;
                    break;
                }
                current = current.parent;
            }
        }

        void PlayAttackAnimation(EventData eventData)
        {
            AttackEventData attackEventData = (AttackEventData)eventData;
            if (attackEventData.weaponUser == caster)
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
