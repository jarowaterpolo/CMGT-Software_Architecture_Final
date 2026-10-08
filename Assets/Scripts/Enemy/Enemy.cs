using JJN.ScriptableObjects;
using PlayerSystem.Inventory;
using PlayerSystem.Movement;
using System;
using UnityEngine;

namespace JJN.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private MoveBehaviour moveBehaviour;
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private IntValue enemyMaxHP;
        private int currentEnemyHP;

        public event Action<int> OnHPChanged;
        public int CurrentHP
        {
            get => currentEnemyHP;
            private set
            {
                if (currentEnemyHP != value)
                {
                    currentEnemyHP = value;
                    OnHPChanged?.Invoke(currentEnemyHP);
                }
            }
        }

        [SerializeField]
        private ItemData weaponData;
        [SerializeField]
        private float attackDelay = 1f;
        private Item weapon;

        public int Money;
        public int XP;

        private SphereCollider attackCollider;
        private float nextAttackTime = 0f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            attackCollider = GetComponent<SphereCollider>();
            weapon = weaponData.CreateItem();
            attackCollider.radius = weapon.Range;
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            moveBehaviour.SetTargetPosition(playerTransform.position);
            CurrentHP = enemyMaxHP.InitialValue;
        }
        void Update()
        {
            moveBehaviour.SetTargetPosition(playerTransform.position);

            if (currentEnemyHP <= 0)
            {
                EventBus<EnemyDieEventData>.Publish(new EnemyDieEventData(this, gameObject));
                Destroy(gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (Time.time >= nextAttackTime)
                {
                    EventBus<AttackEventData>.Publish(new AttackEventData(weapon, gameObject));
                    nextAttackTime = Time.time + attackDelay;
                }
            }
        }

        void GetHit(EventData eventData)
        {
            AttackEventData attackEventData = (AttackEventData)eventData;
            if (attackEventData.weaponUser.CompareTag("Player"))
            {
                if (Vector3.Distance(transform.position, attackEventData.weaponUser.transform.position) < attackEventData.weaponData.Range)
                {
                    CurrentHP -= attackEventData.weaponData.Attack;
                }
            }
        }

        private void OnEnable()
        {
            EventBus<AttackEventData>.OnEventPublished += GetHit; 
        }

        private void OnDisable()
        {
            EventBus<AttackEventData>.OnEventPublished -= GetHit;
        }
    }
}
