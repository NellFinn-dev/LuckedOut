using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject[] objects;

    public void Activate(int selectedObj)
    {
        objects[selectedObj].SetActive(true);
    }

    public void deActivate(int selectedObj)
    {
        objects[selectedObj].SetActive(false);
    }
}
