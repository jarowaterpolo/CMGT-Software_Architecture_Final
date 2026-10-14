using JJN.Calculation;
using JJN.ScriptableObjects;
using JJN.PlayerSystem.Inventory;
using PlayerSystem.Movement;
using System;
using UnityEngine;
using System.Collections;

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

        //enemy hurt effects
        [SerializeField]
        private Renderer _renderer;
        private MaterialPropertyBlock _materialPropertyBlock;


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

            _materialPropertyBlock = new MaterialPropertyBlock();
            _renderer = transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
            _renderer.GetPropertyBlock(_materialPropertyBlock);
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
                    _materialPropertyBlock.SetColor("_BaseColor", new Color(.5f, 0f, 0f, 1f));
                    _renderer.SetPropertyBlock(_materialPropertyBlock);
                    StartCoroutine(ResetColor());
                }
            }
        }

        IEnumerator ResetColor()
        {
            yield return new WaitForSeconds(.2f);
            _materialPropertyBlock.SetColor("_BaseColor", new Color(.5f, .5f, .5f, 1f));
            _renderer.SetPropertyBlock(_materialPropertyBlock);
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
