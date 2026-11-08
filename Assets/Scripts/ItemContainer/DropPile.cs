using UnityEngine;

namespace JJN.ItemSystem
{
    [CreateAssetMenu(fileName = "DropPile", menuName = "Scriptable Objects/DropPile")]
    public class DropPile : ScriptableObject
    {
        public GameObject[] dropPile;
    }
}
