using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PlayerSystem.Inventory
{
    /// <summary>
    /// This is a dummy strategy class, it just convert the item list to an array
    /// and return it without any sorting.
    /// </summary>
    public class ObtainedItemSortingStrategyNewToOld : ItemSortingStrategy
    {
        public override Item[] GetSortedItems(List<Item> items)
        {
            Item[] sortedItems = items.ToArray().Reverse().ToArray();
            return sortedItems;
        }
    }
}
