using JJN.PlayerSystem.Inventory;
using JJN.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace JJN.PlayerSystem.Movement
{
    public class PlayerInventoryController : MonoBehaviour
    {
        [SerializeField]
        private InventoryPresenter inventoryPresenter;

        public bool inInventory = false;
        public UnityEvent openInventory;
        public UnityEvent closeInventory;

        private bool _isDead;

        private void Start()
        {
            // Locks mouse
            CursorOFF();
        }

        private void Update()
        {
            if (_isDead)
            {
                CursorON();
                return;
            }

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

        void SetIsDead(bool isDead)
        {
            _isDead = isDead;
        }

        private void OnEnable()
        {
            PlayerHP.onDeath += SetIsDead;
        }

        private void OnDisable()
        {
            PlayerHP.onDeath -= SetIsDead;
        }
    }
}