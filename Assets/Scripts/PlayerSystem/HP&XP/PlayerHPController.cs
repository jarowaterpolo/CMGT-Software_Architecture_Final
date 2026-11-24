using JJN.Calculation;
using JJN.FSM;
using JJN.ScriptableObjects;
using PlayerSystem.Values;
using UnityEngine;

namespace JJN.PlayerSystem.Values
{
    public class PlayerHPController : MonoBehaviour
    {
        [SerializeField]
        private PlayerHP hp;

        [SerializeField]
        private int MaxHPGainBasedOnLevelUp;

        private bool _isDefending;
        private int _defendValue;

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
                    if (totalDamage > 0 && !PlayerDeathController.IsDead)
                    {
                        hp.HP_value -= totalDamage;
                    }
                }
            }
        }

        void HPPotionUsed(EventData eventData)
        {
            ItemUseEventData itemUseEventData = (ItemUseEventData)eventData;

            if (itemUseEventData.itemData.Id.Contains("potion"))
            {
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

        public void GainMaxHPOnLevelUp(int num)
        {
            hp.Max_HP_value += MaxHPGainBasedOnLevelUp;
            hp.HP_value += MaxHPGainBasedOnLevelUp;
            //Debug.Log($"tried to increase player max hp by {MaxHPGainBasedOnLevelUp}");
            //Debug.Log($"tried to increase player hp by {MaxHPGainBasedOnLevelUp}");
        }

        private void OnEnable()
        {
            PlayerXPController.OnLevelUp += GainMaxHPOnLevelUp;
            EventBus<AttackEventData>.OnEventPublished += GetHit;
            EventBus<ItemUseEventData>.OnEventPublished += HPPotionUsed;
            EventBus<DefendEventData>.OnEventPublished += SetDefendingVars;
        }

        private void OnDisable()
        {
            PlayerXPController.OnLevelUp -= GainMaxHPOnLevelUp;
            EventBus<AttackEventData>.OnEventPublished -= GetHit;
            EventBus<ItemUseEventData>.OnEventPublished -= HPPotionUsed;
            EventBus<DefendEventData>.OnEventPublished -= SetDefendingVars;
        }

        private void OnApplicationQuit()
        {
            hp.SetStartingHP();
        }
    }
}
