using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace JJN.PlayerSystem.Inventory
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
            if (item is Weapon weapon && weapon.Attack != 0)
                attackText.text = weapon.Attack.ToString();
            else
                attackText.text = "";
            if (item is Shield shield && shield.Defense != 0)
                defenseText.text = shield.Defense.ToString();
            else
                defenseText.text = "";
            if (itemCounter != null && item is SpecialItem specialItem && specialItem.isStackable)
            {
                itemCounter.text = "x" + itemCount.ToString();
            }
        }
    }
}
