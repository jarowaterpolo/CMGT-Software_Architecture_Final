using JJN.ItemSystem;
using System;
using UnityEngine;

namespace JJN.FSM
{
    public class AttackState : State
    {
        private Transform _transform;
        public static Action onAttack;
        private float attackStartTime;
        public AttackState(Transform transform, Blackboard pBlackboard)
        {
            _transform = transform;
            blackboard = pBlackboard;
        }
        public override void Enter()
        {
            base.Enter();
            attackStartTime = Time.time;
            onAttack?.Invoke();
            EventBus<AttackEventData>.Publish(new AttackEventData(blackboard.weapon, _transform.gameObject));
            Debug.Log($"{_transform.gameObject.name} is attacking {blackboard.targetTransform.gameObject.name}");
        }
        public bool FinishedAttacking()
        {
            return Time.time > attackStartTime + blackboard.attackDelay;
        }
    }
}
