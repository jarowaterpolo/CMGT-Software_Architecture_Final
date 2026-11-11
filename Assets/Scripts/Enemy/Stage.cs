using UnityEngine;

namespace JJN.Enemy
{
    [CreateAssetMenu(fileName = "Stage", menuName = "Scriptable Objects/Stage")]
    public class Stage : ScriptableObject
    {
        public GameObject[] enemyPrefabs;
        public GameObject bossPrefab;
    }
}
