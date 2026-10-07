using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace PlayerSystem.Inventory
{
    // This class presents items of an inventory in a ListView format.
    public class ListViewInventoryPresenter : InventoryPresenter
    {
        // Indicates whether this inventory belongs to the player
        [SerializeField]
        private bool belongsToPlayer = true;

        // Prefab used to display each item in the inventory list.
        [SerializeField]
        private ItemPresenter itemPresenterPrefab;

        // Parent transform under which item UI elements will be instantiated.
        public Transform listParent;

        // UI text element that displays the name of the current sorting strategy.
        [SerializeField]
        private TextMeshProUGUI sortingStrategyNameText;

        private void OnEnable()
        {
            // If the inventory belongs to the player, get it from the player inventory controller singleton.
            if (belongsToPlayer)
                inventory = SingletonPlayerInventoryController.Instance.inventory;
            PresentInventory();
        }

        // Populates the inventory list UI with sorted items.
        public override void PresentInventory()
        {
            // Clear any existing item UI elements.
            ClearList();

            // Get sorted items from the inventory.
            Item[] items = inventory.GetSortedItems();
            Dictionary<string, int> stackableItemDictionairy = new();
            List<ItemPresenter> itemPresentersWithStackableItems = new();

            // Instantiate and present each item in the UI.
            for (int i = 0; i < items.Length; i++)
            {
                if (!items[i].isStackable)
                {
                    ItemPresenter itemPresenter = Instantiate<ItemPresenter>(itemPresenterPrefab);
                    itemPresenter.PresentItem(items[i], 1);

                    // Set the parent and scale for proper UI layout.
                    itemPresenter.transform.SetParent(listParent);
                    itemPresenter.transform.localScale = Vector3.one;
                }
                else
                {
                    if (!stackableItemDictionairy.ContainsKey(items[i].Id))
                    {
                        stackableItemDictionairy.Add(items[i].Id, 1);
                        ItemPresenter itemPresenter = Instantiate<ItemPresenter>(itemPresenterPrefab);
                        itemPresenter.name = "IconItemPresenter " + items[i].Id;
                        itemPresentersWithStackableItems.Add(itemPresenter);
    
                        itemPresenter.PresentItem(items[i], 1);

                        // Set the parent and scale for proper UI layout.
                        itemPresenter.transform.SetParent(listParent);
                        itemPresenter.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        foreach (ItemPresenter presenter in itemPresentersWithStackableItems)
                        {
                            string presenterNameToSearchFor = "IconItemPresenter " + items[i].Id;
                            if (presenter.name == presenterNameToSearchFor)
                            {
                                stackableItemDictionairy[items[i].Id]++;
                                presenter.PresentItem(items[i], stackableItemDictionairy[items[i].Id]);
                            }
                        }
                    }
                }
            }

            // Update the sorting strategy text if the UI element is assigned.
            if (sortingStrategyNameText != null)
                sortingStrategyNameText.text = inventory.GetCurrentStrategyName();
        }

        // Clears all child item UI elements from the list except the parent itself.
        private void ClearList()
        {
            foreach (Transform transform in listParent.GetComponentsInChildren<Transform>())
            {
                if (transform != listParent)
                    Destroy(transform.gameObject);
            }
        }

        // Handles navigation input for changing the sorting strategy.
        public void Navigate(InputAction.CallbackContext context)
        {
            // Read directional input from the controller/keyboard.
            Vector2 moveVector = context.ReadValue<Vector2>();

            // Navigate to the previous sorting strategy if moving left.
            if (moveVector.x == -1 || Input.GetKeyDown(KeyCode.A))
            {
                RefreshInventoryWithPrevSorting();
            }

            // Navigate to the next sorting strategy if moving right.
            if (moveVector.x == 1 || Input.GetKeyDown(KeyCode.D))
            {
                RefreshInventoryWithNextSorting();
            }
        }
    }
}
