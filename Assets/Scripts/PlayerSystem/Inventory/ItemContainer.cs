using UnityEngine;
using System;
using JJN.Calculation;

namespace JJN.PlayerSystem.Inventory
{
    /// <summary>
    /// An item container that invoke the onGetItem action to
    /// give an item, basicevent bus pattern implemention, which
    /// will be introduced in bootcamp 3.
    /// </summary>
    public class ItemContainer : MonoBehaviour
    {
        public static Action<Item> onGetItem;
        [SerializeField]
        private ItemData[] legendaryLootPile;
        [SerializeField]
        private ItemData[] epicLootPile;
        [SerializeField]
        private ItemData[] rareLootPile;
        [SerializeField]
        private ItemData[] uncommonLootPile;
        [SerializeField]
        private ItemData[] commonLootPile;

        public Item GiveItem()
        {
            string rolledRarity = Calculator.RollRarity();
            Debug.Log($"your rolled rarity = {rolledRarity}");
            var randomNum = 0;
            ItemData itemData = commonLootPile[0];

            switch (rolledRarity)
            {
                case "Legendary":
                    randomNum = UnityEngine.Random.Range(0, legendaryLootPile.Length);
                    itemData = legendaryLootPile[randomNum];
                    break;
                case "Epic":
                    randomNum = UnityEngine.Random.Range(0, epicLootPile.Length);
                    itemData = epicLootPile[randomNum];
                    break;
                case "Rare":
                    randomNum = UnityEngine.Random.Range(0, rareLootPile.Length);
                    itemData = rareLootPile[randomNum];
                    break;
                case "Uncommon":
                    randomNum = UnityEngine.Random.Range(0, uncommonLootPile.Length);
                    itemData = uncommonLootPile[randomNum];
                    break;
                case "Common":
                    randomNum = UnityEngine.Random.Range(0, commonLootPile.Length);
                    itemData = commonLootPile[randomNum];
                    break;
            }

            Item item = itemData.CreateItem();
            onGetItem?.Invoke(item);
            return item;
        }
    }
}
