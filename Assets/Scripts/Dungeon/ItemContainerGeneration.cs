using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace JJNDungeonGeneration
{
    public class ItemContainerGeneration : Generator
    {
        private NewDungeonGenerator dungeonGen;
        private AddFloors addFloors;

        [SerializeField]
        private Transform itemContainersParent;
        [SerializeField]
        private GameObject[] itemContainers;

        private void Start()
        {
            dungeonGen = GetComponent<NewDungeonGenerator>();
            addFloors = GetComponent<AddFloors>();

            dungeonGen.OnStartGeneration += dungeonGen_OnStartGeneration;
            addFloors.OnEndGeneration += addFloors_OnEndGeneration;
        }

        private void dungeonGen_OnStartGeneration()
        {
            for (int i = 0; i < itemContainersParent.childCount; i++)
            {
                Destroy(itemContainersParent.GetChild(i));
            }
        }

        private void addFloors_OnEndGeneration()
        {
            StartCoroutine(SpawnItemContainers());
        }

        private IEnumerator SpawnItemContainers()
        {
            DispatchOnStartGenerationEvent();

            var randomNum = 0;
            var itemContainerSpawnPos = new Vector3(0, 1, 0);
            var randomItemContainer = itemContainers[UnityEngine.Random.Range(0, itemContainers.Length)];
            foreach (var room in dungeonGen.doneRooms)
            {
                randomNum = UnityEngine.Random.Range(0, 100);
                if (randomNum < 33)
                {
                    randomItemContainer = itemContainers[UnityEngine.Random.Range(0, itemContainers.Length)];
                    itemContainerSpawnPos = new(room.x + room.width / 2, 1, room.y + room.height - 2);
                    Instantiate(randomItemContainer, itemContainerSpawnPos, Quaternion.identity, itemContainersParent);
                }
                if (waitingType != WaitingType.Instant) yield return CustomWait(waitingType, splitDelay);
            }

            DispatchOnEndGenerationEvent();
        }
    }
}
