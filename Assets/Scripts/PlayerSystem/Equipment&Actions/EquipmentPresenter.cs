using UnityEngine;

namespace JJN.PlayerSystem.Inventory
{
    /// <summary>
    /// This presenter shows the item model at its world position.
    /// </summary>
    public class EquipmentPresenter : MonoBehaviour
    {
        [SerializeField]
        private Transform weaponTransform;
        [SerializeField]
        private Transform shieldTransform;

        public void PresentItem(Item item, int itemCount)
        {
            GameObject weapon;
            GameObject shield;

            if (item.ItemType == ItemType.Weapon)
            {
                weapon = Instantiate(item.itemModel, weaponTransform.position, weaponTransform.rotation, weaponTransform);
                weapon.transform.localRotation = Quaternion.identity;
            }
            else if (item.ItemType == ItemType.Shield)
            {
                shield = Instantiate(item.itemModel, shieldTransform.position, shieldTransform.rotation, shieldTransform);
                shield.transform.localRotation = Quaternion.identity;
            }
        }
    }
}