using PlayerSystem.Movement;
using UnityEngine;

namespace PlayerSystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private MoveBehaviour moveBehaviour;

        private Transform playerTransform;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            moveBehaviour.SetTargetPosition(playerTransform.position);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
