using PlayerSystem.Values;
using TMPro;
using UnityEngine;

namespace JJN.PlayerSystem.Presenters
{
    public class XPLVLTextPresenter : MonoBehaviour
    {
        private TMP_Text lvlText;
        public void SetLvlText(int lvl, int maxLvl)
        {
            if (lvlText == null)
            {
                lvlText = GetComponent<TMP_Text>();
            }

            if (lvl > maxLvl)
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
            PlayerXPController.OnLevelUp += SetLvlText;
        }

        private void OnDisable()
        {
            PlayerXPController.OnLevelUp -= SetLvlText;
        }
    }
}
