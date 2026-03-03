using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // The main camera reference. If left null, it will find the main camera on Start.
    public Camera mainCamera;

    void Awake()
    {
        // Find the camera with the "MainCamera" tag if not assigned in the Inspector.
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Use LateUpdate to ensure the camera has finished moving for the current frame
    void LateUpdate()
    {
        if (mainCamera != null)
        {
            // This line makes the object look at the camera's position.
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);
        }
    }
}
