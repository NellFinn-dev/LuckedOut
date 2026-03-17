using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomChange : MonoBehaviour
{
    #region instance variable

    public Entity[] enemies;
    public bool[] enemyDowned;
    public GameObject boundaryThisRoom;

    public GameObject boundaryNextRoom;

    public bool doorOpened;

    public bool walkedThrough;
    public bool teleportingRoom;

    public Transform nextRoomSpawnpoint;

    public Animator roomTransitionAnim;
    public roomManager roomManagerScript;

    #endregion

    #region methods 

    private void Awake()
    {
        roomTransitionAnim = GameObject.FindGameObjectWithTag("RoomTransition").GetComponent<Animator>();
        roomManagerScript = GameObject.FindObjectOfType<roomManager>().GetComponent<roomManager>();
    }

    // Check for if all items in an array are of a certain state
    public bool IsAllTrue(bool[] collection)
    {
        for (int i = 0; i < collection.Length; i++)
        {
            if (!collection[i])
            {
                return false;
            }
        }
        return true;
    }

    // Code for the collision trigger for the player to switch rooms
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].health <= 0)
            {
                enemyDowned[i] = true;
            }

        }


        if (IsAllTrue(enemyDowned) && !doorOpened)
        {
            Debug.Log("ALLENEMIESDOWNED!");
            boundaryThisRoom.SetActive(false);
            doorOpened = true;
        }

        if (collision.CompareTag("Player") && doorOpened && !walkedThrough)
        {
            GameObject.FindObjectOfType<roomManager>().roomSelected++;
            boundaryNextRoom.SetActive(true);
            walkedThrough = true;

            if (teleportingRoom)
            {
                roomTransitionAnim.SetTrigger("Trigger");
                GameObject.FindGameObjectWithTag("Player").transform.position = nextRoomSpawnpoint.position;
                roomManagerScript.roomSwitch();
            }
        }
    }
}

        #endregion

    
