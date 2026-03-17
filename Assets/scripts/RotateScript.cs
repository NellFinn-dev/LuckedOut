using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    // Expose a Vector3 to the Editor to control rotation speed on each axis
    [Tooltip("Adjust X, Y, and Z values to control rotation speed on each axis.")]
    public Vector3 rotationAmount;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotate the object every frame
        // Multiply by Time.deltaTime to ensure frame-independent rotation
        transform.Rotate(rotationAmount * Time.deltaTime);
    }
}
