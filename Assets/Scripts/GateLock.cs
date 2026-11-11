using JJN.ItemSystem;
using JJN.PlayerSystem.Movement;
using JJN.ScriptableObjects;
using PlayerSystem.Values;
using UnityEngine;

namespace JJN.PlayerSystem.Inventory
{
    public class GateLock : MonoBehaviour
    {
        private bool _inGateOpenRange;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _inGateOpenRange = true;
            }
        }
        private void OnTriggerExit(Collider other) 
        {
            if (other.CompareTag("Player"))
            {
                _inGateOpenRange = false;
            }
        }
        void KeyUsed(EventData eventData)
        {
            ItemUseEventData itemUseEventData = (ItemUseEventData)eventData;

            Debug.Log($"tried to open gate with {itemUseEventData.itemData.Id}");
            if (itemUseEventData.itemData.Id.Contains("key") && _inGateOpenRange)
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            EventBus<ItemUseEventData>.OnEventPublished += KeyUsed;
        }

        private void OnDisable()
        {
            EventBus<ItemUseEventData>.OnEventPublished -= KeyUsed;
        }
    }
}
