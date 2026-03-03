using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineSpawner : MonoBehaviour
{
    public Transform[] slotMachineSpawnpoints;
    public GameObject slotMachine; // The points are called spawnpoints but the slot machine is already in the level just inactive
    public void SpawnMachine()
    {
        int rand = Random.Range(0, slotMachineSpawnpoints.Length);
        slotMachine.SetActive(true);
        slotMachine.transform.position = slotMachineSpawnpoints[rand].position;
    } 
}
