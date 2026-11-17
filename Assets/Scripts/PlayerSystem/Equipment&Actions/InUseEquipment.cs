using JJN.ItemSystem;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JJN.PlayerSystem.Inventory
{
    [CreateAssetMenu(fileName = "InUseEquipment", menuName = "Scriptable Objects/InUseEquipment")]
    public class InUseEquipment : ScriptableObject
    {
        public Weapon weaponData = null;
        public Shield shieldData = null;
        public SpecialItem itemData = null;

        public bool gotStartingEquipment = false;

#if UNITY_EDITOR
        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }
        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            gotStartingEquipment = false;
        }
#endif
    }
}
