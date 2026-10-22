using System;
using UnityEngine;

namespace JJN.ScriptableObjects
{
    public class PlayerHP
    {
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

        public PlayerHP(int initialMaxHP)
        {
            Max_HP_value = initialMaxHP;
            HP_value = initialMaxHP;
            onDeath?.Invoke(false);
        }
    }
}
