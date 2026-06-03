using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VerticalSelectionMenu : MonoBehaviour
{
    #region Variables
    // This script manages the vertical selection for menu options in the main menu
    // It is applied to each different menu screen (The main one and the level selection one)

    [Header("Input Actions")]
    public InputActionProperty moveAction;
    public InputActionProperty submitAction;

    [Header("Layout Settings")]
    [Tooltip("Fixed vertical distance between items.")]
    public float verticalSpacing = 80f;
    [Tooltip("Starting Y height for the first item.")]
    public float topOffset = 0f;
    [Tooltip("Base horizontal position.")]
    public float horizontalOffset = 0f;

    [Tooltip("How far the selected item shifts to the right.")]
    public float selectedXShift = 50f;

    [Header("Pointer Settings")]
    public RectTransform pointerIcon;
    public Vector2 pointerOffset = new Vector2(-100f, 0f);

    [Header("Visual Settings")]
    public float lerpSpeed = 15f;
    public float idleAlpha = 0.5f;

    public int selectedIndex = 0;
    private RectTransform[] items;

    [Tooltip("Checks this if this is the starting screen.")]
    public bool startingScreen;
    #endregion
    #region Unity Methods

    void Awake()
    {
        if (startingScreen) selectedIndex = 0;

        items = new RectTransform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            items[i] = transform.GetChild(i) as RectTransform;
            if (!items[i].GetComponent<CanvasGroup>())
                items[i].gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnEnable()
    {
        moveAction.action.Enable();
        submitAction.action.Enable();
    }

    public void OnDisable()
    {
        moveAction.action.Disable();
        submitAction.action.Disable();
    }

    void Update()
    {
        HandleInput();

        updatePositions();

        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].GetComponent<MenuOptionBehaviors>() != null)
            {
                items[i].GetComponent<MenuOptionBehaviors>().selected = (i == selectedIndex);
            }
        }
    }

    #endregion
    #region Visuals
    public void updatePositions()
    {
        for (int i = 0; i < items.Length; i++)
        {
            bool isSelected = (i == selectedIndex);

            float targetY = (i * -verticalSpacing) + topOffset;

            float targetX = isSelected ? (horizontalOffset + selectedXShift) : horizontalOffset;

            Vector2 targetPos = new Vector2(targetX, targetY);
            items[i].anchoredPosition = Vector2.Lerp(items[i].anchoredPosition, targetPos, Time.deltaTime * lerpSpeed);

            float targetA = isSelected ? 1.0f : idleAlpha;
            items[i].GetComponent<CanvasGroup>().alpha = Mathf.Lerp(items[i].GetComponent<CanvasGroup>().alpha, targetA, Time.deltaTime * lerpSpeed);

            if (isSelected && pointerIcon != null)
            {
                Vector2 pointerTarget = targetPos + pointerOffset;
                pointerIcon.anchoredPosition = Vector2.Lerp(pointerIcon.anchoredPosition, pointerTarget, Time.deltaTime * lerpSpeed);
            }
        }
    }
    // This method is used to reset each items position when the menu is loaded back up. It's just for nice visual effect
    public void resetPosition()
    {
        for (int i = 0; i < items.Length; i++)
        {
            bool isSelected = (i == selectedIndex);

            float targetY = (i * -verticalSpacing) + topOffset;

            float targetX = isSelected ? (horizontalOffset + selectedXShift) : horizontalOffset;

            Vector2 targetPos = new Vector2(targetX, targetY);
            items[i].anchoredPosition = targetPos;

            float targetA = isSelected ? 1.0f : idleAlpha;
            items[i].GetComponent<CanvasGroup>().alpha = Mathf.Lerp(items[i].GetComponent<CanvasGroup>().alpha, targetA, Time.deltaTime * lerpSpeed);

            if (isSelected && pointerIcon != null)
            {
                Vector2 pointerTarget = targetPos + pointerOffset;
                pointerIcon.anchoredPosition = Vector2.Lerp(pointerIcon.anchoredPosition, pointerTarget, Time.deltaTime * lerpSpeed);
            }
        }
    }

        public IEnumerator menuReset()
    {
        selectedIndex = -1;
        resetPosition();


        yield return new WaitForSeconds(0.25f);
        selectedIndex = 0;
    }
    #endregion  
    #region Input
    private void HandleInput()
    {
        if (moveAction.action.WasPressedThisFrame())
        {
            Vector2 input = moveAction.action.ReadValue<Vector2>();
            if (input.y > 0.5f) selectedIndex = Mathf.Max(0, selectedIndex - 1);
            else if (input.y < -0.5f) selectedIndex = Mathf.Min(items.Length - 1, selectedIndex + 1);
        }

        if (submitAction.action.WasPressedThisFrame())
        {
            Debug.Log("Confirmed: " + items[selectedIndex].name);
        }
    }

    public void Select()
    {
        if (submitAction.action.phase == InputActionPhase.Performed)
        {
            items[selectedIndex].GetComponent<MenuOptionBehaviors>().ActionCalled(); 
        }
    }
    
    #endregion
}