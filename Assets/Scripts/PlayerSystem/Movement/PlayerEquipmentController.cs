using PlayerSystem.Inventory;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerSystem.Inventory
{
    public class PlayerEquipmentController : MonoBehaviour
    {
        [Header("Player Inventory")]
        [SerializeField]
        private Inventory playerInventory;

        [Header("Weapon")]
        [SerializeField]
        private Item weaponData;

        [Header("Shield")]
        [SerializeField]
        private Item shieldData;

        [Header("Item")]
        [SerializeField]
        private Item itemData;

        private void Start()
        {

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (weaponData != null)
                {
                    EventBus<AttackEventData>.Publish(new AttackEventData(weaponData, gameObject));
                }
            }

            if (Input.GetMouseButtonDown(1)) 
            {
                if (shieldData != null) 
                { 
                    EventBus<DefendEventData>.Publish(new DefendEventData(shieldData, gameObject));
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (itemData != null)
                {
                    EventBus<ItemUseEventData>.Publish(new ItemUseEventData(itemData, gameObject));
                }
            }
        }

        public IEnumerator GetStartingEquipment()
        {
            yield return null;
            foreach (Item item in playerInventory.Items)
            {
                if (item.ItemType == ItemType.Weapon)
                {
                    weaponData = item;
                }
            }
        }
    }
}