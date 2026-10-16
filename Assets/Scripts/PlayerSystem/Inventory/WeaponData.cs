using System;
using UnityEngine;

namespace JJN.PlayerSystem.Inventory
{
    public enum WeaponType { Melee, Ranged }
    /// <summary>
    /// This is the script for creating an WeaponData scriptable object, which is
    /// "blueprint" to create weapon objects with the properties set up in the inspector,
    /// it is an implementation of factory pattern.defenseite
    /// </summary>
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
    public class WeaponData : ItemData
    {
        public WeaponType type;
        public float range;
        public int attack;
        public float critChance;
        public float critDamageMult;

        public override Item CreateItem()
        {
            return new Weapon(this);
        }
    }

    [Serializable]
    public class Weapon : Item
    {
        [SerializeField]
        private WeaponType weaponType;
        public WeaponType WeaponType => weaponType;
        [SerializeField]
        private float range;
        public float Range => range;
        [SerializeField]
        private int attack;
        public int Attack => attack;
        [SerializeField]
        private float critChance;
        public float CritChance => critChance;
        [SerializeField]
        private float critDamageMult;
        public float CritDamageMult => critDamageMult;

        public Weapon(WeaponData weaponData) : base(weaponData)
        {
            id = weaponData.id;
            itemName = weaponData.itemName;

            weaponType = weaponData.type;

            range = weaponData.range;
            attack = weaponData.attack;

            critChance = weaponData.critChance;
            critDamageMult = weaponData.critDamageMult;

            itemIcon = weaponData.itemIcon;
            itemModel = weaponData.itemModel;
        }
    }
}
