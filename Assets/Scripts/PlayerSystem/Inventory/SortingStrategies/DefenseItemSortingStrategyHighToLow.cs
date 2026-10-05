using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Player.Inventory
{
    /// <summary>
    /// This is a dummy strategy class, it just convert the item list to an array
    /// and return it without any sorting.
    /// </summary>
    public class DefenseItemSortingStrategyHighToLow : ItemSortingStrategy
    {
        public override Item[] GetSortedItems(List<Item> items)
        {
            //items = items.OrderBy(x => x.Defense).ToList();
            //items.Reverse();
            items = items.OrderByDescending(x => x.Defense).ToList();
            return items.ToArray();
        }
    }
}
