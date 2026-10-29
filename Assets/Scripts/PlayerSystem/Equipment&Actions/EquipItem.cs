using UnityEngine;

namespace JJN.PlayerSystem.Inventory
{
    public class EquipItem : MonoBehaviour
    {
        public Item currentItem;
        
        public void EquipCurrentItem()
        {
            //Debug.Log($"try to equip {currentItem}");
            EventBus<EquipItemEventData>.Publish(new EquipItemEventData(currentItem));
        }
    }
}
