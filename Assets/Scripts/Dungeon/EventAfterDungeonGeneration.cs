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
        private GateGeneration gateGen;

        public UnityEvent TurnOn;
        public UnityEvent TurnOff;

        private void Start()
        {
            dungeonGen = GetComponent<NewDungeonGenerator>();
            gateGen = GetComponent<GateGeneration>();

            dungeonGen.OnStartGeneration += dungeonGen_OnStartGeneration;
            gateGen.OnEndGeneration += GateGeneration_OnEndGeneration;
        }

        private void dungeonGen_OnStartGeneration()
        {
            TurnOff?.Invoke();
        }

        private void GateGeneration_OnEndGeneration()
        {
            TurnOn?.Invoke();  
        }
    }
}
