using JJN.Calculation;
using JJN.ScriptableObjects;
using JJN.PlayerSystem.Inventory;
using System;
using UnityEngine;
using System.Collections;
using JJN.ItemSystem;
using JJN.FSM;

namespace JJN.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private Blackboard blackboard;
        [SerializeField]
        private int patrolWaypointsAmount = 3;
        [SerializeField]
        private Vector2 patrollRange = new(5f, 5f);
        [SerializeField]
        private bool doDebugDraw;
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private EquipmentPresenter equipmentPresenter;
        public EnemyData enemyData;

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
        private EnemyFSM fsm;
        private BossEnemyFSM bossEnemyFSM;

        public void SetStartingHP()
        {
            CurrentHP = enemyData.enemyMaxHP.InitialValue;
            blackboard.maxHP = CurrentHP;
        }

        private Weapon weapon;

        //enemy hurt effects
        [SerializeField]
        private Renderer _renderer;
        private MaterialPropertyBlock _materialPropertyBlock;
        private Color _originalColor;
        [SerializeField]
        private ParticleSystem _critParticles;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            weapon = (Weapon)enemyData.weaponData.CreateItem();
            //attackCollider.radius = weapon.Range;

            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            if (blackboard == null)
            {
                blackboard = GetComponent<Blackboard>();
            }
            blackboard.targetTransform = playerTransform;
            blackboard.weapon = weapon;
            blackboard.attackDelay = enemyData.attackDelay;
            blackboard.doDebugDraw = doDebugDraw;
            CreatePatrolWaypoints();

            if (enemyData.isABoss)
            {
                bossEnemyFSM = new(transform, blackboard);
                bossEnemyFSM.Enter();
            }
            else
            {
                fsm = new(transform, blackboard);
                fsm.Enter();
            }
            //moveBehaviour.SetTargetPosition(playerTransform.position);

            SetStartingHP();

            _materialPropertyBlock = new MaterialPropertyBlock();
            _renderer = transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
            _renderer.GetPropertyBlock(_materialPropertyBlock);

            _originalColor = _renderer.material.color;

            _critParticles = transform.GetChild(1).GetComponent<ParticleSystem>();

            if (equipmentPresenter == null)
            {
                equipmentPresenter = GetComponent<EquipmentPresenter>();
            }

            PresentWeapon();
        }
        void Update()
        {
            if (enemyData.isABoss)
            {
                bossEnemyFSM.Step();
                if (currentEnemyHP <= enemyData.enemyMaxHP.InitialValue * .1f)
                {
                    blackboard.attackDelay = enemyData.attackDelay * .1f;
                }
                else if (currentEnemyHP <= enemyData.enemyMaxHP.InitialValue * .25f)
                {
                    blackboard.attackDelay = enemyData.attackDelay * .25f;
                }
                else if (currentEnemyHP <= enemyData.enemyMaxHP.InitialValue * .5f)
                {
                    blackboard.attackDelay = enemyData.attackDelay * .5f;
                }
            }
            else
            {
                fsm.Step();
            }

            if (currentEnemyHP <= 0)
            {
                EventBus<EnemyDieEventData>.Publish(new EnemyDieEventData(this, gameObject));
                Destroy(gameObject);
            }
        }

        void GetHit(EventData eventData)
        {
            AttackEventData attackEventData = (AttackEventData)eventData;
            if (attackEventData.weaponUser.CompareTag("Player"))
            {
                if (Vector3.Distance(transform.position, attackEventData.weaponUser.transform.position) < attackEventData.weaponData.Range)
                {
                    int damageDealt;
                    bool didCrit;
                    (damageDealt, didCrit) = Calculator.CalculateDamage(attackEventData.weaponData.Attack, attackEventData.weaponData.CritChance, attackEventData.weaponData.CritDamageMult, 0, false);
                    CurrentHP -= damageDealt;

                    blackboard.hp = currentEnemyHP;

                    _materialPropertyBlock.SetColor("_BaseColor", new Color(.5f, 0f, 0f, 1f));
                    _renderer.SetPropertyBlock(_materialPropertyBlock);
                    if (didCrit) 
                    {
                        _critParticles.Play();
                    }
                    StartCoroutine(ResetColor());
                }
            }
        }

        IEnumerator ResetColor()
        {
            yield return new WaitForSeconds(.2f);
            _materialPropertyBlock.SetColor("_BaseColor", _originalColor);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
        }

        void DestorySelf(bool isDead)
        {
            if (!isDead) return;
            Destroy(gameObject);
        }

        public void PresentWeapon()
        {
            equipmentPresenter.PresentItem(weapon, 1);
        }

        private void OnEnable()
        {
            PlayerHP.onDeath += DestorySelf;
            EventBus<AttackEventData>.OnEventPublished += GetHit; 
        }

        private void OnDisable()
        {
            PlayerHP.onDeath -= DestorySelf;
            EventBus<AttackEventData>.OnEventPublished -= GetHit;
        }

        private void OnDrawGizmos()
        {
            if (!blackboard.doDebugDraw) return;
            Gizmos.color = new Color(1f, 0f, 0f, .2f);

            // Draw the sphere.
            Gizmos.DrawSphere(transform.position, blackboard.weapon.Range);

            // Draw wire sphere outline.
            Gizmos.color = new Color(0f, 0f, 1f, .2f);
            Gizmos.DrawSphere(transform.position, blackboard.chaseRange);
        }

        private void CreatePatrolWaypoints()
        {
            Vector3 patrolPoint;
            switch (patrolWaypointsAmount)
            {
                case 3:
                    patrolPoint = new(transform.position.x + UnityEngine.Random.Range(-patrollRange.x, 0), transform.position.y, transform.position.z + UnityEngine.Random.Range(-patrollRange.y, 0));
                    blackboard.waypoints.Add(patrolPoint);
                    patrolPoint = new(transform.position.x + UnityEngine.Random.Range(0, patrollRange.x), transform.position.y, transform.position.z + UnityEngine.Random.Range(-patrollRange.y, 0));
                    blackboard.waypoints.Add(patrolPoint);
                    patrolPoint = new(transform.position.x + UnityEngine.Random.Range(-patrollRange.x, patrollRange.x), transform.position.y, transform.position.z + UnityEngine.Random.Range(0, patrollRange.y));
                    blackboard.waypoints.Add(patrolPoint);
                    break;

                case 4:
                    patrolPoint = new(transform.position.x + UnityEngine.Random.Range(-patrollRange.x, 0), transform.position.y, transform.position.z + UnityEngine.Random.Range(-patrollRange.y, 0));
                    blackboard.waypoints.Add(patrolPoint);
                    patrolPoint = new(transform.position.x + UnityEngine.Random.Range(0, patrollRange.x), transform.position.y, transform.position.z + UnityEngine.Random.Range(-patrollRange.y, 0));
                    blackboard.waypoints.Add(patrolPoint);
                    patrolPoint = new(transform.position.x + UnityEngine.Random.Range(-patrollRange.x, 0), transform.position.y, transform.position.z + UnityEngine.Random.Range(0, patrollRange.y));
                    blackboard.waypoints.Add(patrolPoint);
                    patrolPoint = new(transform.position.x + UnityEngine.Random.Range(0, patrollRange.x), transform.position.y, transform.position.z + UnityEngine.Random.Range(0, patrollRange.y));
                    blackboard.waypoints.Add(patrolPoint);
                    break;

                default:
                    for (int i = 0; i < patrolWaypointsAmount; i++)
                    {
                        patrolPoint = new(transform.position.x + UnityEngine.Random.Range(-patrollRange.x, patrollRange.x), transform.position.y, transform.position.z + UnityEngine.Random.Range(-patrollRange.y, patrollRange.y));
                        blackboard.waypoints.Add(patrolPoint);
                    }
                    break;
            }
        }
    }
}
