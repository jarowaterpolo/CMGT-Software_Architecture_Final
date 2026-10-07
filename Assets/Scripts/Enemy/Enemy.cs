using PlayerSystem.Movement;
using UnityEngine;

namespace JJN.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private MoveBehaviour moveBehaviour;
        [SerializeField]
        private Transform playerTransform;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            moveBehaviour.SetTargetPosition(playerTransform.position);
        }
        void Update()
        {
            moveBehaviour.SetTargetPosition(playerTransform.position);
        }
    }
}
