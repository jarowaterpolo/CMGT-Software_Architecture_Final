using JJN.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JJN.FSM
{
    public class PlayerDeadGUIPresenter : MonoBehaviour
    {
        List<Transform> childs = new();
        private void Start()
        {
            GetChilds();
        }
        void SetGUI(bool GUIInactive)
        {
            if (!GUIInactive)
            {
                foreach (var child in childs)
                {
                    child.gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (var child in childs)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        void GetChilds()
        {
            childs.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                childs.Add(transform.GetChild(i));
            }
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GetChilds();
            SetGUI(PlayerDeathController.IsDead);
        }
        private void OnEnable()
        {
            PlayerHP.onDeath += SetGUI;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            PlayerHP.onDeath -= SetGUI;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
