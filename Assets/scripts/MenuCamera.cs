using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MenuCamera : MonoBehaviour
{
    [Header("Mouse Panning")]
    public float mousePanAmount = 2.5f;
    public float smoothSpeed = 3f;

    [Header("Controller Focus")]
    public float focusStrength = 5f;
    public Canvas menuCanvas;    

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        float mouseX = (mousePos.x / Screen.width) * 2 - 1;
        float mouseY = (mousePos.y / Screen.height) * 2 - 1;
        Quaternion mouseOffset = Quaternion.Euler(-mouseY * mousePanAmount, mouseX * mousePanAmount, 0);

        Quaternion selectionOffset = Quaternion.identity;
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (selected != null)
        {
            Vector3 buttonPos = selected.transform.position;
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 direction = (buttonPos - screenCenter).normalized;

            selectionOffset = Quaternion.Euler(-direction.y * focusStrength, direction.x * focusStrength, 0);
        }

        Quaternion targetRotation = startRotation * mouseOffset * selectionOffset;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}
