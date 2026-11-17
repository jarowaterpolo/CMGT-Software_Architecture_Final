using System;
using UnityEngine;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JJN.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerHP", menuName = "Scriptable Objects/PlayerHP")]
    public class PlayerHP : ScriptableObject
    {
        public int initialMaxHP;

        private int _HP_value;
        private int _maxHP_value;
        public int HP_value
        {
            set
            {
                if (_HP_value != value && value >= 0)
                {
                    _HP_value = value;
                    onHPValueChanged?.Invoke(_HP_value);
                }
                else if (value <= 0)
                {
                    onDeath?.Invoke(true);
                }
            }
            get => _HP_value;
        }
        public static event Action<int> onHPValueChanged;
        public static event Action<bool> onDeath;
        public int Max_HP_value
        {
            set
            {
                if (_maxHP_value != value)
                {
                    _maxHP_value = value;
                    onMaxHPValueChanged?.Invoke(value);
                }
            }
            get => _maxHP_value;
        }
        public static event Action<int> onMaxHPValueChanged;

        private void OnEnable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }
        private void OnDisable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
        }

#if UNITY_EDITOR
        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            SetStartingHP();
        }
#endif
        public void SetStartingHP()
        {
            Max_HP_value = initialMaxHP;
            HP_value = initialMaxHP;
            onDeath?.Invoke(false);
        }
    }
}
