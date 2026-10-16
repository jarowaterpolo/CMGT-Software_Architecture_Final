using System;
using UnityEngine;

namespace JJN.PlayerSystem.Inventory
{
    /// <summary>
    /// This is the script for creating an WeaponData scriptable object, which is
    /// "blueprint" to create weapon objects with the properties set up in the inspector,
    /// it is an implementation of factory pattern.defenseite
    /// </summary>
    [CreateAssetMenu(fileName = "Shield", menuName = "Scriptable Objects/Shield")]
    public class ShieldData : ItemData
    {
        public int defense;

        public override Item CreateItem()
        {
            return new Shield(this);
        }
    }

    [Serializable]
    public class Shield : Item
    {
        [SerializeField]
        private int defense;
        public int Defense => defense;


        public Shield(ShieldData shieldData) : base(shieldData)
        {
            defense = shieldData.defense;
        }
    }
}
