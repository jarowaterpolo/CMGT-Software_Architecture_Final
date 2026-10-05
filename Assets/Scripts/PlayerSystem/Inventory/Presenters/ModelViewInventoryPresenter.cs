using UnityEngine;

namespace PlayerSystem.Inventory
{
    /// <summary>
    /// This class presents items of an inventory as 3D models defined in the item data.
    /// </summary>
    public class ModelViewInventoryPresenter : InventoryPresenter
    {
        [SerializeField]
        private ItemPresenter itemPresenterPrefab;
        [SerializeField]
        private float horizontalDistance = 1f;
        [SerializeField]
        private float verticalDistance = 1f;
        [SerializeField]
        private int horizontalCount = 5;
        [SerializeField]
        private int verticalCount = 2;

        private void Start()
        {
            PresentInventory();
        }

        public override void PresentInventory()
        {
            Item[] items = inventory.GetSortedItems();
            for(int i = 0; i < verticalCount; i++)
            {
                for(int j = 0; j < horizontalCount; j++)
                {
                    ItemPresenter itemPresenter = Instantiate<ItemPresenter>(itemPresenterPrefab);
                    itemPresenter.transform.SetParent(transform);
                    itemPresenter.transform.localPosition = new Vector3(j * horizontalDistance, i * verticalDistance, 0f);
                    itemPresenter.PresentItem(items[i*horizontalCount + j], 1);
                    
                }
            }
        }
    }
}
