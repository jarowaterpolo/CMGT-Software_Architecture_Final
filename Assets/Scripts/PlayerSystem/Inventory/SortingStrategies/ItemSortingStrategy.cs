using System.Collections.Generic;
using UnityEngine;
using System;

namespace JJN.PlayerSystem.Inventory {

    /// <summary>
    /// Abstract class for item sorting strategies.
    /// </summary>
    public abstract class ItemSortingStrategy : MonoBehaviour
    {
        [SerializeField]
        protected string strategyName;
        public string StrategyName => strategyName;
        public abstract Item[] GetSortedItems(List<Item> items);
    }
}
