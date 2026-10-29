using JJN.Calculation;
using JJN.ScriptableObjects;
using PlayerSystem.Values;
using System.Collections;
using TMPro;
using UnityEngine;

namespace JJN.PlayerSystem.Values
{
    public class PlayerHPController : MonoBehaviour
    {
        [SerializeField]
        private int MaxHp;
        private PlayerHP hp;

        [SerializeField]
        private GameObject hurtEffect;

        [SerializeField]
        private int MaxHPGainBasedOnLevelUp;

        private bool _isDefending;
        private int _defendValue;
        private bool isDead;

        private void Start()
        {
            if (hp == null)
            {
                hp = new PlayerHP(MaxHp);
            }
            hurtEffect.SetActive(false);
            SetIsDead(false);
        }
        void GetHit(EventData eventData)
        {
            AttackEventData attackEventData = (AttackEventData)eventData;
            if (attackEventData.weaponUser.CompareTag("Enemy"))
            {
                //Debug.Log($"enemy is trying to attack the player the dist is {Vector3.Distance(transform.position, attackEventData.weaponUser.transform.position)} and the range of the weapon is {attackEventData._weaponData.Range}");
                if (Vector3.Distance(transform.position, attackEventData.weaponUser.transform.position) < attackEventData.weaponData.Range)
                {
                    //Debug.Log($"player got hit by {attackEventData.weaponUser.name} with an {attackEventData._weaponData.ItemName}");
                    //hp.HP_value -= attackEventData._weaponData.Attack;
                    bool didCrit;
                    int totalDamage;
                    (totalDamage, didCrit) = Calculator.CalculateDamage(attackEventData.weaponData.Attack, attackEventData.weaponData.CritChance, attackEventData.weaponData.CritDamageMult, _defendValue, _isDefending);
                    if (totalDamage > 0 && !isDead)
                    {
                        hp.HP_value -= totalDamage;
                        hurtEffect.SetActive(true);
                        StartCoroutine(DisableHurtEffect());
                    }
                }
            }
        }

        IEnumerator DisableHurtEffect()
        {
            yield return new WaitForSeconds(.2f);
            hurtEffect.SetActive(false);
        }

        void HPPotionUsed(EventData eventData)
        {
            ItemUseEventData itemUseEventData = (ItemUseEventData)eventData;
            if (hp.HP_value + itemUseEventData.itemData.SpecialItemValue <= hp.Max_HP_value)
            {
                hp.HP_value += itemUseEventData.itemData.SpecialItemValue;
            }
            else
            {
                var hpGain = hp.Max_HP_value - hp.HP_value;
                hp.HP_value += hpGain;
            }
        }

        void SetDefendingVars(EventData eventData)
        {
            DefendEventData defendEventData = (DefendEventData)eventData;
            if (defendEventData.shieldUser.CompareTag("Player"))
            {
                _isDefending = defendEventData.isBlocking;
                _defendValue = defendEventData.shieldData.Defense;
            }
        }

        void SetIsDead(bool isDead)
        {
            this.isDead = isDead;
            if (isDead)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }

        public void GainMaxHPOnLevelUp(int num)
        {
            hp.Max_HP_value += MaxHPGainBasedOnLevelUp;
            hp.HP_value += MaxHPGainBasedOnLevelUp;
            //Debug.Log($"tried to increase player max hp by {MaxHPGainBasedOnLevelUp}");
            //Debug.Log($"tried to increase player hp by {MaxHPGainBasedOnLevelUp}");
        }

        private void OnEnable()
        {
            PlayerHP.onDeath += SetIsDead;
            PlayerXPController.levelUp += GainMaxHPOnLevelUp;
            EventBus<AttackEventData>.OnEventPublished += GetHit;
            EventBus<ItemUseEventData>.OnEventPublished += HPPotionUsed;
            EventBus<DefendEventData>.OnEventPublished += SetDefendingVars;
        }

        private void OnDisable()
        {
            PlayerHP.onDeath -= SetIsDead;
            PlayerXPController.levelUp -= GainMaxHPOnLevelUp;
            EventBus<AttackEventData>.OnEventPublished -= GetHit;
            EventBus<ItemUseEventData>.OnEventPublished -= HPPotionUsed;
            EventBus<DefendEventData>.OnEventPublished -= SetDefendingVars;
        }
    }
}
