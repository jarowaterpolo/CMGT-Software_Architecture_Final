using JJN.PlayerSystem.Movement;
using JJN.ScriptableObjects;
using System.Collections;
using System.Linq;
using UnityEngine;

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

        private Weapon _weaponData = null;
        private Shield _shieldData = null;
        private SpecialItem _itemData = null;

        private bool _isCurrentlyBlocking = false;
        private bool _isDead;

        private void Start()
        {
            StartCoroutine(GetStartingEquipment());
        }

        private void Update()
        {
            if (!inventoryController.inInventory && !_isDead)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_weaponData != null)
                    {
                        EventBus<AttackEventData>.Publish(new AttackEventData(_weaponData, gameObject));
                        //Debug.Log($"{_weaponData.ItemName} used to attack");
                    }
                }


                if (_shieldData != null)
                {
                    if (Input.GetMouseButtonDown(1) && !_isCurrentlyBlocking)
                    {
                        EventBus<DefendEventData>.Publish(new DefendEventData(_shieldData, gameObject, true));
                        //Debug.Log($"using {_shieldData.ItemName} to defend");
                        _isCurrentlyBlocking = true;
                    }

                    if (Input.GetMouseButtonUp(1) && _isCurrentlyBlocking)
                    {
                        EventBus<DefendEventData>.Publish(new DefendEventData(_shieldData, gameObject, false));
                        //Debug.Log($"stopped defending");
                        _isCurrentlyBlocking = false;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (_itemData != null)
                    {
                        EventBus<ItemUseEventData>.Publish(new ItemUseEventData(_itemData, gameObject));
                        //Debug.Log($"{_itemData.ItemName} used");
                        _itemData = SearchForSameItem(_itemData);
                    }
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    EventBus<OpenItemContainerEventData>.Publish(new OpenItemContainerEventData(transform));
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
                        _weaponData = weapon;
                        equipmentPresenter.PresentItem(item, 1);
                        //Debug.Log($"{item.ItemName} copied to _weaponData");
                        break;

                    case Shield shield:
                        _shieldData = shield;
                        equipmentPresenter.PresentItem(item, 1);
                        //Debug.Log($"{item.ItemName} copied to _shieldData");
                        break;

                    case SpecialItem specialItem:
                        _itemData = specialItem;
                        //Debug.Log($"{item.ItemName} copied to _itemData");
                        break;
                }
            }
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
                    _weaponData = weapon;
                    equipmentPresenter.PresentItem(weapon, 1);
                    break;
                case Shield shield:
                    equipmentPresenter.PresentItem(shield, 1);
                    _shieldData = shield;
                    break;
                case SpecialItem specialItem:
                    _itemData = specialItem;
                    break;
            }

            //Debug.Log($"{equipItemEventData.itemToEquip} was just equiped");
        }

        void SetIsDead(bool isDead)
        {
            this._isDead = isDead;
        }

        private void OnEnable()
        {
            PlayerHP.onDeath += SetIsDead;
            EventBus<EquipItemEventData>.OnEventPublished += EquipItem;
        }

        private void OnDisable()
        {
            EventBus<EquipItemEventData>.OnEventPublished -= EquipItem;
        }
    }
}