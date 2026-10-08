using PlayerSystem.Inventory;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerSystem.Inventory
{
    public class PlayerEquipmentController : MonoBehaviour
    {
        [SerializeField]
        private Inventory playerInventory;
        [SerializeField]
        private EquipmentPresenter equipmentPresenter;

        private Item weaponData = null;
        private Item shieldData = null;
        private Item itemData = null;

        private bool isCurrentlyBlocking = false;

        private void Start()
        {
            StartCoroutine(GetStartingEquipment());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (weaponData != null)
                {
                    EventBus<AttackEventData>.Publish(new AttackEventData(weaponData, gameObject));
                    Debug.Log($"{weaponData.ItemName} used to attack");
                }
            }


            if (shieldData != null)
            {
                if (Input.GetMouseButtonDown(1) && !isCurrentlyBlocking)
                {
                    EventBus<DefendEventData>.Publish(new DefendEventData(shieldData, gameObject, true));
                    Debug.Log($"using {shieldData.ItemName} to defend");
                    isCurrentlyBlocking = true;
                }

                if (Input.GetMouseButtonUp(1) && isCurrentlyBlocking)
                {
                    EventBus<DefendEventData>.Publish(new DefendEventData(shieldData, gameObject, false));
                    Debug.Log($"stopped defending");
                    isCurrentlyBlocking = false;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (itemData != null)
                {
                    EventBus<ItemUseEventData>.Publish(new ItemUseEventData(itemData, gameObject));
                    Debug.Log($"{itemData.ItemName} used");
                    itemData = SearchForSameItem(itemData);
                }
            }
        }

        public IEnumerator GetStartingEquipment()
        {
            yield return null;
            foreach (Item item in playerInventory.Items)
            {
                if (item == null) continue;

                //Debug.Log($"{item.ItemName} is a {item.ItemType}");

                switch (item.ItemType)
                {
                    case ItemType.Weapon:
                        weaponData = item;
                        equipmentPresenter.PresentItem(item, 1);
                        //Debug.Log($"{item.ItemName} copied to weaponData");
                        break;

                    case ItemType.Shield:
                        shieldData = item;
                        equipmentPresenter.PresentItem(item, 1);
                        //Debug.Log($"{item.ItemName} copied to shieldData");
                        break;

                    case ItemType.Potion:
                        itemData = item;
                        //Debug.Log($"{item.ItemName} copied to itemData");
                        break;
                }
            }
        }

        public Item SearchForSameItem(Item itemToSearchFor)
        {
            foreach (Item item in playerInventory.Items)
            {
                if (item == null) continue;

                if (item.Id == itemToSearchFor.Id)
                {
                    Debug.Log($"{item.ItemName} was found to replace{itemToSearchFor.ItemName}");
                    return item;
                }
            }
            return null;
        }
    }
}