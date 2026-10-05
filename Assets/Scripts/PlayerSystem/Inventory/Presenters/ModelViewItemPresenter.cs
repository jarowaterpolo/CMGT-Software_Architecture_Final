using UnityEngine;

namespace Player.Inventory
{
    /// <summary>
    /// This presenter shows the item model at its world position.
    /// </summary>
    public class ModelViewItemPresenter : ItemPresenter
    {
        public override void PresentItem(Item item, int itemCount)
        {
            GameObject itemModel = Instantiate(item.itemModel);
            itemModel.transform.position = transform.position;
            itemModel.transform.SetParent(transform);
        }
    }
}
