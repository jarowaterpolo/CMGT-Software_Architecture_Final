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

        void DeleteOldEquipment(Item item)
        {
            if (item is Weapon weapon)
            {
                if (weaponTransform.childCount == 0) return;
                Destroy(weaponTransform.GetChild(0).gameObject);
            }
            else if (item is Shield shield)
            {
                if (shieldTransform.childCount == 0) return;
                Destroy(shieldTransform.GetChild(0).gameObject);
            }
        }
        public void PresentItem(Item item, int itemCount)
        {
            DeleteOldEquipment(item);

            GameObject weaponObject;
            GameObject shieldObject;

            if (item is Weapon weapon)
            {
                weaponObject = Instantiate(item.itemModel, weaponTransform.position, weaponTransform.rotation, weaponTransform);
                weaponObject.transform.localRotation = Quaternion.identity;
            }
            else if (item is Shield shield)
            {
                shieldObject = Instantiate(item.itemModel, shieldTransform.position, shieldTransform.rotation, shieldTransform);
                shieldObject.transform.localRotation = Quaternion.identity;
            }
        }
    }
}