using JJN.PlayerSystem.Values;
using JJN.ScriptableObjects;
using PlayerSystem.Values;
using System;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JJN.PlayerSystem.Presenters
{
    public class XPBarPresenter : MonoBehaviour
    {
        private Image xpBar;

        public void SetXPBarValue(int currentXP, int maxXP)
        {
            if (xpBar == null)
            {
                xpBar = GetComponent<Image>();
                xpBar.fillAmount = 0f;
            }
            else
            {
                xpBar.fillAmount = (float)currentXP / (float)maxXP;
            }
        }

        private void OnEnable()
        {
            PlayerXPController.OnXPChange += SetXPBarValue;
        }

        private void OnDisable()
        {
            PlayerXPController.OnXPChange -= SetXPBarValue;
        }
    }
}
