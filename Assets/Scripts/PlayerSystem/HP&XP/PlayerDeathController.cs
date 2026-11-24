using JJN.ScriptableObjects;
using UnityEngine;

namespace JJN.FSM
{
    public class PlayerDeathController : MonoBehaviour
    {
        public static bool IsDead = false;

        void SetIsDead(bool isDead)
        {
            IsDead = isDead;

            if (isDead)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }

        private void OnEnable()
        {
            PlayerHP.onDeath += SetIsDead;
        }

        private void OnDisable()
        {
            PlayerHP.onDeath -= SetIsDead;
        }
    }
}
