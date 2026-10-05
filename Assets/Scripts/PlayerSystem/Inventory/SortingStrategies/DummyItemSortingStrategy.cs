using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PlayerSystem.Inventory
{
    /// <summary>
    /// This is a dummy strategy class, it just convert the item list to an array
    /// and return it without any sorting.
    /// </summary>
    public class DummyItemSortingStrategy : ItemSortingStrategy
    {
        public override Item[] GetSortedItems(List<Item> items)
        {
            return items.ToArray();
        }
    }
}
