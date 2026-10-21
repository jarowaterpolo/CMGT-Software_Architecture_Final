using JJN.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace JJN.PlayerSystem.Presenters
{
    public class HPPresenter : MonoBehaviour
    {
        [SerializeField]
        private HP_ScriptableObject hpObject;

        private TMP_Text hpText;

        void Start()
        {
            hpText = GetComponent<TMP_Text>();
        }

        void UpdateHPText(int hp)
        {
            hpText.text = $"{hp}/{hpObject.Max_HP_value}HP";
        }
        private void OnEnable()
        {
            hpObject.onHPValueChanged += UpdateHPText;
        }

        private void OnDisable()
        {
            hpObject.onHPValueChanged -= UpdateHPText;
        }
    }
}
