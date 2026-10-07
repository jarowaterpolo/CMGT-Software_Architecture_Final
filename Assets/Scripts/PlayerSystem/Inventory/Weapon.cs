using PlayerSystem.Inventory;
using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField]
        private ItemData itemData;
    }
}
