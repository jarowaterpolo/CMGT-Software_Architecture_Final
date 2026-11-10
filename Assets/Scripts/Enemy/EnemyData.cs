using JJN.ScriptableObjects;
using System;
using UnityEngine;
using JJN.ItemSystem;

namespace JJN.PlayerSystem.Inventory
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public IntValue enemyMaxHP;
        public int XP;
        public WeaponData weaponData;
        public float attackDelay = 1f;
        public float itemDropChance = 0f;
    }
}
