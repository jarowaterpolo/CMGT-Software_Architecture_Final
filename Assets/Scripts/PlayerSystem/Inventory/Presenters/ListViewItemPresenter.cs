using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Player.Inventory
{
    /// <summary>
    /// This class presents an item in a list in the GUI.
    /// </summary>
    public class ListViewItemPresenter : ItemPresenter
    {
        public Image icon;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI attackText;
        public TextMeshProUGUI defenseText;
        public TMP_Text itemCounter;

        public override void PresentItem(Item item, int itemCount)
        {
            icon.sprite = item.itemIcon;
            nameText.text = item.ItemName;
            if (item.Attack != 0)
                attackText.text = item.Attack.ToString();
            else
                attackText.text = "";
            if (item.Defense != 0)
                defenseText.text = item.Defense.ToString();
            else
                defenseText.text = "";
            if (itemCounter != null && item.isStackable)
            {
                itemCounter.text = "x" + itemCount.ToString();
            }
        }
    }
}
