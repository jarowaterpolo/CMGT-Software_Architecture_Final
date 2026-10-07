using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem.Inventory
{
    public enum ItemType { Weapon, Shield, Helmet, Chestplate, Leggings, Boots, Potion}
    /// <summary>
    /// This is the script for creating an ItemData scriptable object, which is
    /// "blueprint" to create item objects with the properties set up in the inspector,
    /// it is an implementation of factory pattern.defenseite
    /// </summary>
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class ItemData : ScriptableObject
    {
        [Header("Unique id for each item")]
        public string id;

        [Header("Core properties")]
        public string itemName;
        public ItemType type;
        public int attack;
        public int defense;

        [Header("Visuals")]
        public Sprite itemIcon;
        public GameObject itemModel;
        public bool isStackable;

        public Item CreateItem()
        {
            return new Item(this);
        }
    }

    [Serializable]
    public class Item
    {
        [Header("Unique id for each item")]
        [SerializeField]
        private string id;
        public string Id => id;//This setup allows access to the private field 'id'
                               //while also allows it to be shown in the inspector

        [Header("Core properties")]
        [SerializeField]
        private string itemName;
        public string ItemName => itemName;
        [SerializeField]
        private ItemType itemType;
        public ItemType ItemType => itemType;
        [SerializeField]
        private int attack;
        public int Attack => attack;
        [SerializeField]
        private int defense;
        public int Defense => defense;

        [Header("Visuals")]
        public Sprite itemIcon;
        public GameObject itemModel;
        public bool isStackable;

        public Item(ItemData itemData)
        {
            id = itemData.id;
            itemName = itemData.itemName;

            itemType = itemData.type;

            attack = itemData.attack;
            defense = itemData.defense;

            itemIcon = itemData.itemIcon;
            itemModel = itemData.itemModel;
            isStackable = itemData.isStackable;
        }
    }
}
