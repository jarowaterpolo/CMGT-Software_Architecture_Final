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
        private AddFloors addFloors;

        public UnityEvent TurnOn;
        public UnityEvent TurnOff;

        private void Start()
        {
            dungeonGen = GetComponent<NewDungeonGenerator>();
            addFloors = GetComponent<AddFloors>();

            dungeonGen.OnStartGeneration += dungeonGen_OnStartGeneration;
            addFloors.OnEndGeneration += addFloors_OnEndGeneration;
        }

        private void dungeonGen_OnStartGeneration()
        {
            TurnOff?.Invoke();
        }

        private void addFloors_OnEndGeneration()
        {
            TurnOn?.Invoke();  
        }
    }
}
