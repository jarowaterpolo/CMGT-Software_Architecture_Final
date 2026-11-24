using JJN.FSM;
using JJN.PlayerSystem.Inventory;
using JJN.QuestSystem.Quests;
using JJN.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JJN.QuestSystem
{
    public class SingletonQuestController : MonoBehaviour
    {
        public static SingletonQuestController Instance { get; private set; }

        [SerializeField]
        private List<Quest> quests;
        private int _questIndex = 0;
        private Quest _currentQuest;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            if (quests == null || quests.Count == 0) return;

            foreach (var quest in quests)
            {
                if (quest.RegisterEarly)
                {
                    quest.Register();
                }
            }

            InitializeCurrentQuest();
        }

        void Update()
        {
            if (_currentQuest == null || _currentQuest.IsComplete || PlayerDeathController.IsDead) return;

            _currentQuest.Evaluate();

            if (_currentQuest.IsComplete)
            {
                //Debug.Log($"Quest '{_currentQuest.QuestName}' completed!");
                EventBus<QuestEventData>.Publish(new(_currentQuest, true));
                AdvanceToNextQuest();
            }
            else
            {
                //EventBus<QuestEventData>.Publish(new(_currentQuest, false));
#if UNITY_EDITOR
                if(Input.GetKeyDown(KeyCode.F1)){
                    AdvanceToNextQuest();
                }
#endif
            }
        }

        void InitializeCurrentQuest()
        {
            if (_questIndex < quests.Count)
            {
                _currentQuest = quests[_questIndex];
                _currentQuest.Initialize();
                EventBus<QuestEventData>.Publish(new(_currentQuest, false));
                //Debug.Log($"started quest: {_currentQuest.QuestName}");
            }
            else
            {
                _currentQuest = null;
                DummyQuest allQuestDone = DummyQuest.Create("All quests completed");
                EventBus<QuestEventData>.Publish(new(allQuestDone, false));
                //Debug.Log("All quests Completed!");
            }
        }

        void AdvanceToNextQuest()
        {
            _questIndex++;
            InitializeCurrentQuest();
        }

        void SetIsDead(bool isDead)
        {
            if (isDead)
            {
                DummyQuest playerDead = DummyQuest.Create("DIED");
                EventBus<QuestEventData>.Publish(new(playerDead, false));
                //Debug.Log("DIED!");
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_currentQuest != null)
            {
                EventBus<QuestEventData>.Publish(new(_currentQuest, false));
            }
            else
            {
                DummyQuest allQuestDone = DummyQuest.Create("All quests completed");
                EventBus<QuestEventData>.Publish(new(allQuestDone, false));
            }
        }

        private void OnEnable()
        {
            if (Instance != this) return;

            PlayerHP.onDeath += SetIsDead;
            SceneManager.sceneLoaded += OnSceneLoaded;
            
        }

        private void OnDisable()
        {
            if (Instance != this) return;

            PlayerHP.onDeath -= SetIsDead;
            SceneManager.sceneLoaded -= OnSceneLoaded;

            if (quests == null) return;

            foreach (var quest in quests)
            {
                quest.Unregister();
            }
        } 
    }
}
