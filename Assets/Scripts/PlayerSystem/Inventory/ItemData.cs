using System;
using UnityEngine;

namespace JJN.PlayerSystem.Inventory
{
    public class ItemData : ScriptableObject
    {
        [Header("Unique id for each item")]
        public string id;

        [Header("Core properties")]
        public string itemName;

        [Header("Visuals")]
        public Sprite itemIcon;
        public GameObject itemModel;

        public virtual Item CreateItem()
        {
            return new Item(this);
        }
    }

    [Serializable]
    public class Item
    {
        [Header("Unique id for each item")]
        [SerializeField]
        protected string id;
        public string Id => id;//This setup allows access to the private field 'id'
                               //while also allows it to be shown in the inspector

        [Header("Core properties")]
        [SerializeField]
        protected string itemName;
        public string ItemName => itemName;

        [Header("Visuals")]
        public Sprite itemIcon;
        public GameObject itemModel;

        public Item(ItemData itemData)
        {
            id = itemData.id;
            itemName = itemData.itemName;

            itemIcon = itemData.itemIcon;
            itemModel = itemData.itemModel;
        }
    }
}
