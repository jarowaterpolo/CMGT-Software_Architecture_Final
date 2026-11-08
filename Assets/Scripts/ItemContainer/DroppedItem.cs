using System;
using UnityEngine;

namespace JJN.ItemSystem
{
    public class DroppedItem : MonoBehaviour
    {
        public static Action<Item> onPickupItem;

        [SerializeField]
        private ItemData itemData;

        private bool inPickupRange = false;

        private void OnEnable()
        {
            EventBus<PickupItemEventData>.OnEventPublished += Pickup;
        }

        private void OnDisable()
        {
            EventBus<PickupItemEventData>.OnEventPublished -= Pickup;
        }

        public void Pickup(EventData eventData)
        {
            //OpenItemContainerEventData openItemContainerEventData = (OpenItemContainerEventData)eventData;

            if (inPickupRange)
            {
                Item item = itemData.CreateItem();
                onPickupItem?.Invoke(item);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                inPickupRange = true;
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                inPickupRange = false;
            }
        }
    }
}
