using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Player.Inventory
{
    /// <summary>
    /// Todo: finish this class, then replace this header with your own class description.
    /// </summary>
    public class IconViewItemPresenter : ItemPresenter
    {
        public Image icon;
        public TMP_Text itemCounter;
        private Item currentItem;
        private int currentItemCount;

        public override void PresentItem(Item item, int itemCount)
        {
            //todo: implement this method to present the item as an icon
            //in the grid
            var gridImage = icon.GetComponent<Image>();
            gridImage.sprite = item.itemIcon;
            if (itemCounter != null && item.isStackable)
            {
                currentItemCount = itemCount;
                itemCounter.text = "x" + itemCount.ToString();
            }
            currentItem = item;
        }

        public void DisplayItemInfo()
        {
            //todo: display the item info by modifying ItemInfoDisplayer.itemInfo
            //var itemInfoDisplayer = GetComponent<ItemInfoDisplayer>();
            //itemInfoDisplayer;
            ItemInfoDisplayer.itemInfo = $"{currentItem.ItemName} \n";

            if (currentItem.Attack > 0)
            {
                ItemInfoDisplayer.itemInfo += $"{currentItem.Attack} \n";
            }
            if (currentItem.Defense > 0)
            {
                ItemInfoDisplayer.itemInfo += $"{currentItem.Defense} \n";
            }
            if (currentItem.isStackable && currentItemCount > 1)
            {
                ItemInfoDisplayer.itemInfo += $"you have {currentItemCount} of them";
            }
        }

        public void ClearItemInfo()
        {
            ItemInfoDisplayer.itemInfo = "";
        }
    }
}
