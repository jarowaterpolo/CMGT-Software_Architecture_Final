using System;
using UnityEngine;

namespace JJN.ScriptableObjects
{
    [CreateAssetMenu(fileName = "IntValue", menuName = "Scriptable Objects/IntValue")]
    public class IntValue : ScriptableObject
    {
        [SerializeField]
        private int initialValue;
        public int InitialValue => initialValue;
    }
}
