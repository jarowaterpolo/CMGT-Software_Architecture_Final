using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace JJNDungeonGeneration
{
    public class GateGeneration : Generator
    {
        private NewDungeonGenerator dungeonGen;
        private ItemContainerGeneration itemContainerGen;

        [SerializeField]
        private Transform dungeonParent;
        [SerializeField]
        private GameObject gate;

        private GameObject spawnedGate;

        private void Start()
        {
            dungeonGen = GetComponent<NewDungeonGenerator>();
            itemContainerGen = GetComponent<ItemContainerGeneration>();

            dungeonGen.OnStartGeneration += dungeonGen_OnStartGeneration;
            itemContainerGen.OnEndGeneration += itemContainerGen_OnEndGeneration;
        }

        private void dungeonGen_OnStartGeneration()
        {
            Destroy(spawnedGate);
        }

        private void itemContainerGen_OnEndGeneration()
        {
            SpawnGate();
        }

        private void SpawnGate()
        {
            DispatchOnStartGenerationEvent();

            var randomRoom = dungeonGen.doneRooms[UnityEngine.Random.Range(1, dungeonGen.doneRooms.Count)];
            
            Vector3 gateSpawnPos = new(randomRoom.x + randomRoom.width / 2, 2.5f, randomRoom.y + randomRoom.height - 2);
            spawnedGate = Instantiate(gate, gateSpawnPos, Quaternion.identity, dungeonParent);


            DispatchOnEndGenerationEvent();
        }
    }
}
