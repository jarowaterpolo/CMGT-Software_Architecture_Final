using UnityEngine;

namespace JJN.ItemSystem
{
    [CreateAssetMenu(fileName = "LootPile", menuName = "Scriptable Objects/LootPile")]
    public class LootPile : ScriptableObject
    {
        public ItemData[] lootPile;
    }
}
