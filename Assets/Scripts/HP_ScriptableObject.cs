using System;
using UnityEngine;

namespace JJN.ScriptableObjects
{
    [CreateAssetMenu(fileName = "HP_ScriptableObject", menuName = "Scriptable Objects/HP_ScriptableObject")]
    public class HP_ScriptableObject : ScriptableObject
    {
        [SerializeField]
        private int initialMaxHPValue;

        //Whenever value is changed, it invoke the onValueChanged event to notify
        //all the subscribers, this is a very basic use of observer pattern
        private int _HP_value;
        private int _maxHP_value;
        public int HP_value
        {
            set
            {
                if (_HP_value != value)
                {
                    _HP_value = value;
                    onHPValueChanged?.Invoke(_HP_value);
                }
            }
            get => _HP_value;
        }
        public event Action<int> onHPValueChanged;
        public int Max_HP_value
        {
            set
            {
                if (_maxHP_value != value)
                {
                    _maxHP_value = value;
                    onMaxHPValueChanged?.Invoke();
                }
            }
            get => _maxHP_value;
        }
        public event Action onMaxHPValueChanged;

        private void OnEnable()
        {
            Max_HP_value = initialMaxHPValue;
            HP_value = initialMaxHPValue;
        }
    }
}
