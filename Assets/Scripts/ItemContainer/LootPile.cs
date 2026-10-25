using UnityEngine;

namespace JJN.PlayerSystem.Inventory
{
    [CreateAssetMenu(fileName = "LootPile", menuName = "Scriptable Objects/LootPile")]
    public class LootPile : ScriptableObject
    {
        public ItemData[] lootPile;
    }
}
