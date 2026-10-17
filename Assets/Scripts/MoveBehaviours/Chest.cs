using UnityEngine;
using JJN.PlayerSystem.Inventory;
using System;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the chests in 2D scene, give an item when opened if the chest has
/// an ItemContainer component.
/// </summary>
public class Chest : MonoBehaviour
{
    [SerializeField]
    private bool readyToOpen = false;
    private bool opened = false;

    private void OnEnable()
    {
        EventBus<OpenItemContainerEventData>.OnEventPublished += Open;
    }

    private void OnDisable()
    {
        EventBus<OpenItemContainerEventData>.OnEventPublished -= Open;
    }

    public void Open(EventData eventData)
    {
        //OpenItemContainerEventData openItemContainerEventData = (OpenItemContainerEventData)eventData;

        if (readyToOpen && !opened)
        {
            readyToOpen = false;
            opened = true;

            ItemContainer itemContainer = GetComponent<ItemContainer>();
            if(itemContainer != null)
            {
                Item item = itemContainer.GiveItem();
                Debug.Log($"you got an {item.ItemName}");
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            readyToOpen = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            readyToOpen = false;
        }
    }
}
