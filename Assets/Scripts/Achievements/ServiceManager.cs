using UnityEngine;
/// <summary>
/// Manager services for the service locator
/// </summary>
namespace JJN.Achievements
{
    public class ServiceManager : MonoBehaviour
    {
        void Awake()
        {
            InitServices();
        }
        public void InitServices()
        {
            ServiceLocator.RegisterService<IAchievementService>(GetComponent<IAchievementService>());
        }

        public void UnregisterServices()
        {
            ServiceLocator.UnregisterService<IAchievementService>(GetComponent<IAchievementService>());
        }

        void OnDestroy()
        {
            UnregisterServices();
        }
    }
}