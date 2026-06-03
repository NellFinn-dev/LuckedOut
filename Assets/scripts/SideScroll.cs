using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll : MonoBehaviour
{
    #region variables
    public float ScrollSpeed = -0.5f;
    public Vector2 _savedOffset;
    public Renderer _renderer;
    #endregion

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _savedOffset = _renderer.material.mainTextureOffset;
    }

    private void Update()
    {
        float x = Mathf.Repeat(Time.time * ScrollSpeed, 1);
        Vector2 offset = new Vector2(x, 0);
        _renderer.material.mainTextureOffset = offset;
    }

    private void OnDisable()
    {
        _renderer.material.mainTextureOffset = _savedOffset;
    }
}
