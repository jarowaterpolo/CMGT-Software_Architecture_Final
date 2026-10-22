using JJN.ScriptableObjects;
using System;
using UnityEngine;

namespace JJN.PlayerSystem.Inventory
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public IntValue enemyMaxHP;
        public int Money;
        public int XP;
        public WeaponData weaponData;
        public float attackDelay = 1f;
    }
}
