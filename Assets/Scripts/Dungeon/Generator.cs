namespace JJNDungeonGeneration
{
    using NaughtyAttributes;
    using System;
    using System.Collections;
    using UnityEngine;

    public abstract class Generator : MonoBehaviour
    {
        public event Action OnStartGeneration;
        public event Action OnEndGeneration;

        public event Action OnNeededRepeat;
        public enum WaitingType
        {
            Instant, Overtime, Space
        }

        public WaitingType waitingType;
        public float splitDelay = 0.05f;
        public Color[] colors = { Color.green, Color.red, Color.cyan, Color.black, new Color(255, 175, 0, 1), Color.blue };

        public AudioSource audioSource;

        public bool DoDebugDraw;

        //[Space(100)]

        protected void DispatchOnStartGenerationEvent()
        {
            OnStartGeneration?.Invoke();
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }

        protected void DispatchOnEndGenerationEvent()
        {
            OnEndGeneration?.Invoke();
        }

        protected void DispatchOnNeededRepeatEvent()
        {
            OnNeededRepeat?.Invoke();
        }

        public IEnumerator CustomWait(WaitingType splitType, float splitDelay)
        {
            switch (splitType)
            {
                case WaitingType.Overtime:
                    yield return new WaitForSeconds(splitDelay);
                    break;
                case WaitingType.Space:
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                    yield return null;
                    break;
            }
        }
    }
}