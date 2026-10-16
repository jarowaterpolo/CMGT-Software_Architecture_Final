using JJN.Calculation;
using JJN.ScriptableObjects;
using System.Collections;
using TMPro;
using UnityEngine;

namespace JJN.PlayerSystem.Values
{
    public class PlayerHPController : MonoBehaviour
    {
        [SerializeField]
        private HP_ScriptableObject hpObject;
        [SerializeField]
        private TMP_Text hpText;
        [SerializeField]
        private GameObject hurtEffect;

        private bool _isDefending;
        private int _defendValue;

        private void Start()
        {
            hurtEffect.SetActive(false);
        }
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
                    bool didCrit;
                    int totalDamage;
                    (totalDamage, didCrit) = DamageCalculator.CalculateDamage(attackEventData.weaponData.Attack, attackEventData.weaponData.CritChance, attackEventData.weaponData.CritDamageMult, _defendValue, _isDefending);
                    if (totalDamage > 0)
                    {
                        hpObject.HP_value -= totalDamage;
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
