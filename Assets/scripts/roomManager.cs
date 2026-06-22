using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManager : MonoBehaviour
{
    #region instance variabes 
    public int roomSelected;
    public GameObject[] roomTops;
    //public Transform[] camSpots;

    public GameObject cam;
    //public bool camSwitching;
    public bool roomTopsOn;

    #endregion

    #region methods 

    public void roomSwitch()
    {
        // Sets the cam positions based on the room the player is currently in
        /*
        if (camSwitching)
        {
            cam.transform.position = (camSpots[roomSelected].position + GameObject.FindGameObjectWithTag("Player").transform.position)/2f;
        }
        */
        if (roomTopsOn)
        {
            for (int i = 0; i < roomTops.Length; i++)
            {
                if (i == roomSelected)
                {
                    roomTops[i].SetActive(true);
                }
                else
                {
                    roomTops[i].SetActive(false);
                }
            }
        }
    }

    #endregion
}
