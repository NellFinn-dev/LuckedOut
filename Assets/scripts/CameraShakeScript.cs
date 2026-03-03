using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeScript : MonoBehaviour
{
    #region instance variables

    public float D;
    public float M;
    public Vector3 thing;

    public int trigger;

    #endregion

    #region methods

    // Sets the trigger variable to 1
    public void triggerShake()
    {
        trigger = 1;
    }

    // if trigger is = to 1 then the shaking of the camera happens
    private void Update()
    {
        if(trigger == 1)
        {
            shake();
            trigger = 0;
        }
    }

    // Some math that handles the camera shaking
    /*
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapse = 0.0f;
        thing = originalPos;

        while (elapse < duration)
        {
            float xPos = Random.Range(originalPos.x -.5f, originalPos.x + .5f) * magnitude;
            float yPos = Random.Range(originalPos.y - .5f, originalPos.y + .5f) * magnitude;

            transform.position = new Vector3(xPos, yPos, originalPos.z);

            elapse += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = new Vector2(0, 0);
    }
    */
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = (Mathf.PerlinNoise(Time.time * 20f, 0f) - 0.5f) * magnitude;
            float y = (Mathf.PerlinNoise(0f, Time.time * 20f) - 0.5f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }


    // The method that calls the coroutine for shaking
    public void shake()
    {
        StartCoroutine(Shake(D, M));
    }

    #endregion
}

