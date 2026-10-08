using System;
using UnityEngine;

namespace JJN.ScriptableObjects
{
    [CreateAssetMenu(fileName = "IntValueWithAction", menuName = "Scriptable Objects/IntValueWithAction")]
    public class IntValueWithAction : ScriptableObject
    {
        [SerializeField]
        private int initialValue;

        //Whenever value is changed, it invoke the onValueChanged event to notify
        //all the subscribers, this is a very basic use of observer pattern
        private int _value;
        public int value
        {
            set
            {
                if (_value != value)
                {
                    _value = value;
                    onValueChanged?.Invoke();
                }
            }
            get => _value;
        }
        public event Action onValueChanged;

        private void OnEnable()
        {
            value = initialValue;
        }
    }
}
