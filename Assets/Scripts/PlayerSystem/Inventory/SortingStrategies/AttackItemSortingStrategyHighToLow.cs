using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PlayerSystem.Inventory
{
    /// <summary>
    /// This is a dummy strategy class, it just convert the item list to an array
    /// and return it without any sorting.
    /// </summary>
    public class AttackItemSortingStrategyHighToLow : ItemSortingStrategy
    {
        public override Item[] GetSortedItems(List<Item> items)
        {
            items = items.OrderByDescending(x => x.Attack).ToList();
            return items.ToArray();
        }
    }
}
