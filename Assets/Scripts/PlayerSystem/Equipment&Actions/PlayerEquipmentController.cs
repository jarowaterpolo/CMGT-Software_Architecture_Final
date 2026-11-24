using JJN.FSM;
using JJN.ItemSystem;
using JJN.PlayerSystem.Movement;
using JJN.ScriptableObjects;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JJN.PlayerSystem.Inventory
{
    public class PlayerEquipmentController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInventoryController inventoryController;
        [SerializeField]
        private Inventory playerInventory;
        [SerializeField]
        private EquipmentPresenter equipmentPresenter;
        [SerializeField]
        private InUseEquipment inUseEquipment;

        private bool _isCurrentlyBlocking = false;

        private void Start()
        {
            if (inUseEquipment.gotStartingEquipment == false)
            {
                StartCoroutine(GetStartingEquipment());
            }
        }

        private void Update()
        {
            if (!inventoryController.inInventory && !PlayerDeathController.IsDead)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (inUseEquipment.weaponData != null)
                    {
                        EventBus<AttackEventData>.Publish(new AttackEventData(inUseEquipment.weaponData, gameObject));
                        //Debug.Log($"{_weaponData.ItemName} used to attack");
                    }
                }


                if (inUseEquipment.shieldData != null)
                {
                    if (Input.GetMouseButtonDown(1) && !_isCurrentlyBlocking)
                    {
                        EventBus<DefendEventData>.Publish(new DefendEventData(inUseEquipment.shieldData, gameObject, true));
                        //Debug.Log($"using {_shieldData.ItemName} to defend");
                        _isCurrentlyBlocking = true;
                    }

                    if (Input.GetMouseButtonUp(1) && _isCurrentlyBlocking)
                    {
                        EventBus<DefendEventData>.Publish(new DefendEventData(inUseEquipment.shieldData, gameObject, false));
                        //Debug.Log($"stopped defending");
                        _isCurrentlyBlocking = false;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (inUseEquipment.itemData != null)
                    {
                        EventBus<ItemUseEventData>.Publish(new ItemUseEventData(inUseEquipment.itemData, gameObject));
                        //Debug.Log($"{inUseEquipment.itemData.ItemName} used");
                        inUseEquipment.itemData = SearchForSameItem(inUseEquipment.itemData);
                    }
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    EventBus<OpenItemContainerEventData>.Publish(new OpenItemContainerEventData(transform));
                    EventBus<PickupItemEventData>.Publish(new PickupItemEventData(transform));
                    //Debug.Log("open item container event published");
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

                switch (item)
                {
                    case Weapon weapon:
                        inUseEquipment.weaponData = weapon;
                        equipmentPresenter.PresentItem(item, 1);
                        //Debug.Log($"{item.ItemName} copied to _weaponData");
                        break;

                    case Shield shield:
                        inUseEquipment.shieldData = shield;
                        equipmentPresenter.PresentItem(item, 1);
                        //Debug.Log($"{item.ItemName} copied to _shieldData");
                        break;

                    case SpecialItem specialItem:
                        inUseEquipment.itemData = specialItem;
                        //Debug.Log($"{item.ItemName} copied to _itemData");
                        break;
                }
            }
            inUseEquipment.gotStartingEquipment = true;
        }

        public SpecialItem SearchForSameItem(SpecialItem itemToSearchFor)
        {
            foreach (SpecialItem item in playerInventory.Items.OfType<SpecialItem>())
            {
                if (item == null) continue;

                if (item.Id == itemToSearchFor.Id)
                {
                    //Debug.Log($"{item.ItemName} was found to replace{itemToSearchFor.ItemName}");
                    return item;
                }
            }
            return null;
        }

        void EquipItem(EventData eventData)
        {
            EquipItemEventData equipItemEventData = (EquipItemEventData)eventData;

            switch (equipItemEventData.itemToEquip)
            {
                case Weapon weapon:
                    inUseEquipment.weaponData = weapon;
                    equipmentPresenter.PresentItem(weapon, 1);
                    break;
                case Shield shield:
                    equipmentPresenter.PresentItem(shield, 1);
                    inUseEquipment.shieldData = shield;
                    break;
                case SpecialItem specialItem:
                    inUseEquipment.itemData = specialItem;
                    break;
            }

            //Debug.Log($"{equipItemEventData.itemToEquip.ItemName} was just equiped");
        }

        void ResetPlayerEquipment()
        {
            StartCoroutine(GetStartingEquipment());
        }

        private void OnEnable()
        {
            EventBus<EquipItemEventData>.OnEventPublished += EquipItem;
            PlayerDeathController.OnPlayerReset += ResetPlayerEquipment;
        }

        private void OnDisable()
        {
            EventBus<EquipItemEventData>.OnEventPublished -= EquipItem;
            PlayerDeathController.OnPlayerReset -= ResetPlayerEquipment;
        }
    }
}