using JJN.Calculation;
using JJN.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace PlayerSystem.Values
{
    public class PlayerHPController : MonoBehaviour
    {
        [SerializeField]
        private HP_ScriptableObject hpObject;
        [SerializeField]
        private TMP_Text hpText;

        private bool _isDefending;
        private int _defendValue;

        void GetHit(EventData eventData)
        {
            AttackEventData attackEventData = (AttackEventData)eventData;
            if (attackEventData.weaponUser.CompareTag("Enemy"))
            {
                //Debug.Log($"enemy is trying to attack the player the dist is {Vector3.Distance(transform.position, attackEventData.weaponUser.transform.position)} and the range of the weapon is {attackEventData.weaponData.Range}");
                if (Vector3.Distance(transform.position, attackEventData.weaponUser.transform.position) < attackEventData.weaponData.Range)
                {
                    //Debug.Log($"player got hit by {attackEventData.weaponUser.name} with an {attackEventData.weaponData.ItemName}");
                    //hpObject.HP_value -= attackEventData.weaponData.Attack;
                    hpObject.HP_value -= DamageCalculator.CalculateDamage(attackEventData.weaponData.Attack, _defendValue, _isDefending);
                }
            }
        }

        void HPPotionUsed(EventData eventData)
        {
            ItemUseEventData itemUseEventData = (ItemUseEventData)eventData;
            if (hpObject.HP_value + itemUseEventData.itemData.SpecialItemValue <= hpObject.Max_HP_value)
            {
                hpObject.HP_value += itemUseEventData.itemData.SpecialItemValue;
            }
            else
            {
                var hpGain = hpObject.Max_HP_value - hpObject.HP_value;
                hpObject.HP_value += hpGain;
            }
        }

        void UpdateHPText(int hp)
        {
            hpText.text = $"{hp}/{hpObject.Max_HP_value}HP";
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


        private void OnEnable()
        {
            hpObject.onHPValueChanged += UpdateHPText;
            EventBus<AttackEventData>.OnEventPublished += GetHit;
            EventBus<ItemUseEventData>.OnEventPublished += HPPotionUsed;
            EventBus<DefendEventData>.OnEventPublished += SetDefendingVars;
        }

        private void OnDisable()
        {
            hpObject.onHPValueChanged -= UpdateHPText;
            EventBus<AttackEventData>.OnEventPublished -= GetHit;
            EventBus<ItemUseEventData>.OnEventPublished -= HPPotionUsed;
            EventBus<DefendEventData>.OnEventPublished -= SetDefendingVars;
        }
    }
}
