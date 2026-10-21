using JJN.ScriptableObjects;
using PlayerSystem.Values;
using TMPro;
using UnityEngine;

namespace JJN.PlayerSystem.Presenters
{
    public class XPLVLTextPresenter : MonoBehaviour
    {
        [SerializeField]
        private PlayerXPController playerXPController;
        private TMP_Text lvlText;

        void Start()
        {
            lvlText = GetComponent<TMP_Text>();
        }

        public void SetLvlText(int lvl)
        {
            if (lvl > playerXPController.xpNeededForEachLevel.Length)
            {
                lvlText.text = $"MaxLvl";
            }
            else
            {
                lvlText.text = $"Lvl:{lvl}";
            }
        }
        private void OnEnable()
        {
            playerXPController.currentLevel.onValueChanged += SetLvlText;
        }

        private void OnDisable()
        {
            playerXPController.currentLevel.onValueChanged -= SetLvlText;
        }
    }
}
