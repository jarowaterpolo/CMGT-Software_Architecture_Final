using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace JJN.PlayerSystem.Inventory
{
    /// <summary>
    /// This is a dummy strategy class, it just convert the item list to an array
    /// and return it without any sorting.
    /// </summary>
    public class DefenseItemSortingStrategyHighToLow : ItemSortingStrategy
    {
        public override Item[] GetSortedItems(List<Item> items)
        {
            var sorted = items.OrderByDescending(x => (x as Shield)?.Defense ?? 0).ToList();
            return items.ToArray();
        }
    }
}
