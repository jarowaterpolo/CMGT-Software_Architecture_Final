using UnityEngine;

namespace JJN.PlayerSystem.Inventory
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
            GameObject weapon;
            GameObject shield;

            if (item.ItemType == ItemType.Weapon)
            {
                weapon = Instantiate(item.itemModel, transform.position + weaponOffset, transform.rotation, transform);
                weapon.transform.localRotation = Quaternion.identity;
            }
            else if (item.ItemType == ItemType.Shield)
            {
                shield = Instantiate(item.itemModel, transform.position + shieldOffset, transform.rotation, transform);
                shield.transform.localRotation = Quaternion.identity;
            }
        }
    }
}