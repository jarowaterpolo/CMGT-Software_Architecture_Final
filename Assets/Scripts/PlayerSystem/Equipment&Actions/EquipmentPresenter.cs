using JJN.ItemSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JJN.PlayerSystem.Inventory
{
    /// <summary>
    /// This presenter shows the item model at its world position.
    /// </summary>
    public class EquipmentPresenter : MonoBehaviour
    {
        [SerializeField]
        private InUseEquipment inUseEquipment;
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

            //Debug.Log($"present item called with item {item.ItemName}");

            GameObject weaponObject;
            GameObject shieldObject;

            if (item is Weapon weapon)
            {
                //Debug.Log($"Weapon Presented called {weapon.ItemName}");
                //Debug.Log($"weapon transform = {weaponTransform}");
                weaponObject = Instantiate(item.itemModel, weaponTransform.position, weaponTransform.rotation, weaponTransform);
                weaponObject.transform.localRotation = Quaternion.identity;
            }
            else if (item is Shield shield)
            {
                //Debug.Log($"Shield Presented called {shield.ItemName}");
                shieldObject = Instantiate(item.itemModel, shieldTransform.position, shieldTransform.rotation, shieldTransform);
                shieldObject.transform.localRotation = Quaternion.identity;
            }
        }

        private void OnEnable()
        {
            if (inUseEquipment == null) return;
            PresentItem(inUseEquipment.weaponData, 1);
            PresentItem(inUseEquipment.shieldData, 1);
            //Debug.Log($"in use equipment: Weapon = {inUseEquipment.weaponData.ItemName}, Shield = {inUseEquipment.shieldData.ItemName}");
        }
    }
}