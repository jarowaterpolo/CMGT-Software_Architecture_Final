using JJN.ScriptableObjects;
using PlayerSystem.Values;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JJN.PlayerSystem.Presenters
{
    public class XPLVLTextPresenter : MonoBehaviour
    {
        [SerializeField]
        private PlayerXPController playerXPController;
        private TMP_Text lvlText;
        public void SetLvlText(int lvl)
        {
            if (lvlText == null)
            {
                lvlText = GetComponent<TMP_Text>();
            }

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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            playerXPController.currentLevel.onValueChanged -= SetLvlText;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SetLvlText(playerXPController.currentLevel.value);
        }
    }
}
