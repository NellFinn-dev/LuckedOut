using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Reference to the object
    public GameObject refObject;
    //Methods
    #region methods
    // Does pretty much what it says it does
    public void deSpawn()
    {
        Destroy(refObject);
    }

    #endregion

}
