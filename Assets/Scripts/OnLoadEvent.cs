using UnityEngine;
using UnityEngine.Events;

namespace PlayerSystem
{
    public class OnLoadEvent : MonoBehaviour
    {
        public UnityEvent loadEvent;

        private void Start()
        {
            loadEvent?.Invoke();
        }
    }
}
