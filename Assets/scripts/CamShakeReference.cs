using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakeReference : MonoBehaviour
{
    // A script than can be used to trigger a camera shake
    // Orignially created for the "GO!" animation at the beginning of each level

    #region method(s)

    public void Shake()
    {
        // Sets the CameraShakeScript trigger variable to 1
        GameObject.FindObjectOfType<CameraShakeScript>().trigger = 1;
    }

    #endregion
}
