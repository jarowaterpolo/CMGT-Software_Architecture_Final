using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JJN.PlayerSystem.Inventory
{
    public class Gate : MonoBehaviour
    {
        [SerializeField]
        private float timeToWait = 5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventBus<AdvanceStageEventData>.Publish(new AdvanceStageEventData(other.transform));
                StartCoroutine(ReloadScene());
            }
        }

        IEnumerator ReloadScene()
        {
            yield return new WaitForSeconds(timeToWait);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
