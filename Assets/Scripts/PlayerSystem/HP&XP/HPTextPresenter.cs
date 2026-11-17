using JJN.QuestSystem.Quests;
using JJN.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JJN.PlayerSystem.Presenters
{
    public class HPPresenter : MonoBehaviour
    {
        [SerializeField]
        private PlayerHP hp;

        private TMP_Text hpText;

        void Start()
        {
            if (hpText == null)
            {
                hpText = GetComponent<TMP_Text>();
            }
        }

        void UpdateHPText(int num)
        {
            if (hpText == null)
            {
                hpText = GetComponent<TMP_Text>();
            }
            hpText.text = $"{hp.HP_value}/{hp.Max_HP_value}HP";
        }

        private void OnEnable()
        {
            PlayerHP.onHPValueChanged += UpdateHPText;
            PlayerHP.onMaxHPValueChanged += UpdateHPText;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            PlayerHP.onHPValueChanged -= UpdateHPText;
            PlayerHP.onMaxHPValueChanged -= UpdateHPText;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            UpdateHPText(0);
        }
    }
}
