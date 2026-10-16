using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace JJN.PlayerSystem.Inventory
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
            if (itemCounter != null && item is SpecialItem specialItem && specialItem.isStackable)
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

            if (currentItem is Weapon weapon && weapon.Attack > 0)
            {
                ItemInfoDisplayer.itemInfo += $"{weapon.Attack} Attack \n";
            }
            if (currentItem is Shield shield && shield.Defense > 0)
            {
                ItemInfoDisplayer.itemInfo += $"{shield.Defense} Defense \n";
            }
            if (currentItem is SpecialItem specialItem && specialItem.isStackable && currentItemCount > 1)
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
