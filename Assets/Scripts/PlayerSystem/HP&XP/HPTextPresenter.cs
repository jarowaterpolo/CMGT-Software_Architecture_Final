using JJN.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace JJN.PlayerSystem.Presenters
{
    public class HPPresenter : MonoBehaviour
    {
        private int currentMaxHP;

        private TMP_Text hpText;

        void Start()
        {
            hpText = GetComponent<TMP_Text>();
        }

        void UpdateHPText(int hp)
        {
            hpText.text = $"{hp}/{currentMaxHP}HP";
        }

        void SetCurrentMaxHP(int maxHPValue)
        {
            currentMaxHP = maxHPValue;
        }
        private void OnEnable()
        {
            PlayerHP.onHPValueChanged += UpdateHPText;
            PlayerHP.onMaxHPValueChanged += SetCurrentMaxHP;
        }

        private void OnDisable()
        {
            PlayerHP.onHPValueChanged -= UpdateHPText;
            PlayerHP.onMaxHPValueChanged -= SetCurrentMaxHP;
        }
    }
}
