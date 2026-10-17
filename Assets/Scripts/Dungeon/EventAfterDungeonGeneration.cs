using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace JJNDungeonGeneration
{
    public class EventAfterDungeonGeneration : Generator
    {
        private NewDungeonGenerator dungeonGen;
        private ItemContainerGeneration itemContainerGeneration;

        public UnityEvent TurnOn;
        public UnityEvent TurnOff;

        private void Start()
        {
            dungeonGen = GetComponent<NewDungeonGenerator>();
            itemContainerGeneration = GetComponent<ItemContainerGeneration>();

            dungeonGen.OnStartGeneration += dungeonGen_OnStartGeneration;
            itemContainerGeneration.OnEndGeneration += itemContainerGeneration_OnEndGeneration;
        }

        private void dungeonGen_OnStartGeneration()
        {
            TurnOff?.Invoke();
        }

        private void itemContainerGeneration_OnEndGeneration()
        {
            TurnOn?.Invoke();  
        }
    }
}
