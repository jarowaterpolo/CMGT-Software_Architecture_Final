using PlayerSystem.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerSystem.Movement
{
    public class PlayerInventoryController : MonoBehaviour
    {
        [SerializeField]
        private InventoryPresenter inventoryPresenter;

        public bool inInventory = false;
        public UnityEvent openInventory;
        public UnityEvent closeInventory;

        private void Start()
        {
            // Locks mouse
            CursorOFF();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                openInventory?.Invoke();
                ToggleInInventory();
            }

            if (inInventory)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    inventoryPresenter.RefreshInventoryWithPrevSorting();
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    inventoryPresenter.RefreshInventoryWithNextSorting();
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    closeInventory?.Invoke();
                    ToggleInInventory();
                }
            }
        }

        public void ToggleInInventory()
        {
            inInventory = !inInventory;
            if (inInventory)
            {
                CursorON();
            }
            else
            {
                CursorOFF();
            }
        }

        public void CursorON()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void CursorOFF() 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}