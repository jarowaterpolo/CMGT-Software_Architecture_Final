using JJN.ItemSystem;
using JJN.PlayerSystem.Movement;
using JJN.ScriptableObjects;
using PlayerSystem.Values;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace JJN.PlayerSystem.Inventory
{
    public class GateLock : MonoBehaviour
    {
        [SerializeField]
        private float fallSpeed;
        [SerializeField]
        private float targetDifferendeInY;
        private bool _inGateOpenRange;
        private bool _keyUsed;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _inGateOpenRange = true;

                TryOpenGate();
            }
        }
        private void OnTriggerExit(Collider other) 
        {
            if (other.CompareTag("Player"))
            {
                _inGateOpenRange = false;
            }
        }
        void KeyUsed(EventData eventData)
        {
            ItemUseEventData itemUseEventData = (ItemUseEventData)eventData;

            //Debug.Log($"tried to open gate with {itemUseEventData.itemData.Id}");
            if (itemUseEventData.itemData.Id.Contains("key"))
            {
                _keyUsed = true;
                TryOpenGate();
            }
        }

        void TryOpenGate()
        {
            if (_inGateOpenRange && _keyUsed)
            {
                StartCoroutine(OpenGateEffect());
            }
        }

        IEnumerator OpenGateEffect()
        {
            yield return null;
            Vector3 startPos = transform.position;
            //Debug.Log($"position difference = {transform.position.y} > {startPos.y} +{targetDifferendeInY} = {transform.position.y > startPos.y + targetDifferendeInY}");
            while (transform.position.y > startPos.y + targetDifferendeInY)
            {
                yield return null;
                //Debug.Log($"moving towards {startPos.y + targetDifferendeInY}");
                Vector3 fallPosition = Vector3.MoveTowards(transform.position, transform.position - new Vector3(0, startPos.y + targetDifferendeInY, 0), fallSpeed * Time.deltaTime);
                transform.position = fallPosition;
            }
            Destroy(gameObject);
        }

        private void OnEnable()
        {
            EventBus<ItemUseEventData>.OnEventPublished += KeyUsed;
        }

        private void OnDisable()
        {
            EventBus<ItemUseEventData>.OnEventPublished -= KeyUsed;
        }
    }
}
