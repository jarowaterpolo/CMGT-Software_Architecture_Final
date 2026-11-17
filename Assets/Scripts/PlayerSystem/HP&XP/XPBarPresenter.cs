using JJN.ScriptableObjects;
using PlayerSystem.Values;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JJN.PlayerSystem.Presenters
{
    public class XPBarPresenter : MonoBehaviour
    {
        [SerializeField]
        private PlayerXPController playerXPController;

        private Image xpBar;
        public void SetXPBarValue(int XP)
        {
            if (xpBar == null)
            {
                xpBar = GetComponent<Image>();
                xpBar.fillAmount = 0f;
            }
            if (playerXPController.currentLevel.value == playerXPController.xpNeededForEachLevel.Length + 1)
            {
                xpBar.fillAmount = 1f;
            }
            else
            {
                xpBar.fillAmount = (float)XP / (float)playerXPController.xpNeededForEachLevel[playerXPController.currentLevel.value - 1];
            }
        }

        private void OnEnable()
        {
            playerXPController.currentXP.onValueChanged += SetXPBarValue;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            playerXPController.currentXP.onValueChanged -= SetXPBarValue;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SetXPBarValue(playerXPController.currentXP.value);
        }
    }
}
