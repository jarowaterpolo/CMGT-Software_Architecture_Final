using UnityEngine;

namespace PlayerSystem.Inventory
{
    /// <summary>
    /// This presenter shows the item model at its world position.
    /// </summary>
    public class EquipmentPresenter : MonoBehaviour
    {
        [SerializeField]
        private Vector3 weaponOffset;
        [SerializeField]
        private Vector3 shieldOffset;

        public void PresentItem(Item item, int itemCount)
        {
            GameObject itemModel = Instantiate(item.itemModel);
            itemModel.transform.position = transform.position;
            if (item.ItemType == ItemType.Weapon)
            {
                itemModel.transform.position += weaponOffset;
            }
            else if (item.ItemType == ItemType.Shield)
            {
                itemModel.transform.position += shieldOffset;
            }
            itemModel.transform.SetParent(transform);
        }
    }
}