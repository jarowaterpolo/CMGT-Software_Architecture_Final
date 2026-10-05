using UnityEngine;

namespace PlayerSystem.Inventory {

    /// <summary>
    /// Abstract item presenter, an item presenter presents an item as different
    /// visuals in the game.
    /// </summary>
    public abstract class ItemPresenter : MonoBehaviour
    {
        public abstract void PresentItem(Item item, int itemCount);
    }
}
