using System;
using UnityEngine;

namespace JJN.PlayerSystem.Inventory
{
    /// <summary>
    /// This is the script for creating an WeaponData scriptable object, which is
    /// "blueprint" to create weapon objects with the properties set up in the inspector,
    /// it is an implementation of factory pattern.defenseite
    /// </summary>
    [CreateAssetMenu(fileName = "Special Item", menuName = "Scriptable Objects/Special Item")]
    public class SpecialItemData : ItemData
    {
        public int specialItemValue;
        public bool isStackable;

        public override Item CreateItem()
        {
            return new SpecialItem(this);
        }
    }

    [Serializable]
    public class SpecialItem : Item
    {
        [SerializeField]
        private int specialItemValue;
        public int SpecialItemValue => specialItemValue;

        public bool isStackable;

        public SpecialItem(SpecialItemData specialItemData) : base(specialItemData)
        {
            specialItemValue = specialItemData.specialItemValue;
            isStackable = specialItemData.isStackable;
        }
    }
}
