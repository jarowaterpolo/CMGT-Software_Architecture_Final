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
        private LootPile legendary;
        [SerializeField]
        private LootPile epic;
        [SerializeField]
        private LootPile rare;
        [SerializeField]
        private LootPile uncommon;
        [SerializeField]
        private LootPile common;

        public Item GiveItem()
        {
            string rolledRarity = Calculator.RollRarity();
            Debug.Log($"your rolled rarity = {rolledRarity}");
            var randomNum = 0;
            ItemData itemData = common.lootPile[0];

            switch (rolledRarity)
            {
                case "Legendary":
                    randomNum = UnityEngine.Random.Range(0, legendary.lootPile.Length);
                    itemData = legendary.lootPile[randomNum];
                    break;
                case "Epic":
                    randomNum = UnityEngine.Random.Range(0, epic.lootPile.Length);
                    itemData = epic.lootPile[randomNum];
                    break;
                case "Rare":
                    randomNum = UnityEngine.Random.Range(0, rare.lootPile.Length);
                    itemData = rare.lootPile[randomNum];
                    break;
                case "Uncommon":
                    randomNum = UnityEngine.Random.Range(0, uncommon.lootPile.Length);
                    itemData = uncommon.lootPile[randomNum];
                    break;
                case "Common":
                    randomNum = UnityEngine.Random.Range(0, common.lootPile.Length);
                    itemData = common.lootPile[randomNum];
                    break;
            }

            Item item = itemData.CreateItem();
            onGetItem?.Invoke(item);
            return item;
        }
    }
}
