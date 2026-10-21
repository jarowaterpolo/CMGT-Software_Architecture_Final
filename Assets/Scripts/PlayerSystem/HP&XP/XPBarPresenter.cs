using JJN.ScriptableObjects;
using PlayerSystem.Values;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JJN.PlayerSystem.Presenters
{
    public class XPBarPresenter : MonoBehaviour
    {
        [SerializeField]
        private PlayerXPController playerXPController;

        private Image xpBar;

        private void Start()
        {
            xpBar = GetComponent<Image>();
            xpBar.fillAmount = 0f;
        }

        public void SetXPBarValue(int XP)
        {
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
        }

        private void OnDisable()
        {
            playerXPController.currentXP.onValueChanged -= SetXPBarValue;
        }
    }
}
