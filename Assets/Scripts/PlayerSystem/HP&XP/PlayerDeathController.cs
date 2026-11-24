using JJN.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JJN.FSM
{
    public class PlayerDeathController : MonoBehaviour
    {
        public static bool IsDead = false;
        public static Action OnPlayerReset;

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

        public void ResetPlayer()
        {
            SetIsDead(false);
            OnPlayerReset.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
