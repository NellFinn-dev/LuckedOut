using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VerticalSelectionMenu : MonoBehaviour
{
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

    public int _selectedIndex = 0;
    private RectTransform[] _items;

    private void OnEnable()
    {
        moveAction.action.Enable();
        submitAction.action.Enable();
    }

    void Start()
    {
        _items = new RectTransform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _items[i] = transform.GetChild(i) as RectTransform;
            if (!_items[i].GetComponent<CanvasGroup>())
                _items[i].gameObject.AddComponent<CanvasGroup>();
        }
    }

    void Update()
    {
        HandleInput();

        for (int i = 0; i < _items.Length; i++)
        {
            bool isSelected = (i == _selectedIndex);

            float targetY = (i * -verticalSpacing) + topOffset;

            float targetX = isSelected ? (horizontalOffset + selectedXShift) : horizontalOffset;

            Vector2 targetPos = new Vector2(targetX, targetY);
            _items[i].anchoredPosition = Vector2.Lerp(_items[i].anchoredPosition, targetPos, Time.deltaTime * lerpSpeed);

            float targetA = isSelected ? 1.0f : idleAlpha;
            _items[i].GetComponent<CanvasGroup>().alpha = Mathf.Lerp(_items[i].GetComponent<CanvasGroup>().alpha, targetA, Time.deltaTime * lerpSpeed);

            if (isSelected && pointerIcon != null)
            {
                Vector2 pointerTarget = targetPos + pointerOffset;
                pointerIcon.anchoredPosition = Vector2.Lerp(pointerIcon.anchoredPosition, pointerTarget, Time.deltaTime * lerpSpeed);
            }
        }
    }

    private void HandleInput()
    {
        if (moveAction.action.WasPressedThisFrame())
        {
            Vector2 input = moveAction.action.ReadValue<Vector2>();
            if (input.y > 0.5f) _selectedIndex = Mathf.Max(0, _selectedIndex - 1);
            else if (input.y < -0.5f) _selectedIndex = Mathf.Min(_items.Length - 1, _selectedIndex + 1);
        }

        if (submitAction.action.WasPressedThisFrame())
        {
            Debug.Log("Confirmed: " + _items[_selectedIndex].name);
        }
    }

    public void Select()
    {
        if(_selectedIndex == 0)
        {
            Debug.Log("Play");
        }
    }
}