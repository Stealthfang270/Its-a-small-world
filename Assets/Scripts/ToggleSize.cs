using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSize : MonoBehaviour
{
    public Vector3 bigScale;
    public Vector3 smallScale;
    Vector3 targetScale;
    Vector3 oldScale;
    public float scaleTime;
    float lerpFloat;

    public bool isBig;

    private void Start()
    {
        EventManager.toggle.AddListener(OnToggle);
        if (isBig)
        {
            targetScale = bigScale;
        }
        else
        {
            targetScale = smallScale;
        }
        oldScale = targetScale;
    }

    private void Update()
    {
        if (transform.localScale != targetScale)
        {
            lerpFloat = Mathf.Clamp01(lerpFloat + Time.deltaTime / scaleTime + 0.001f);

            transform.localScale = Vector3.Lerp(oldScale, targetScale, lerpFloat);
        }

    }

    void OnToggle()
    {
        isBig = !isBig;
        lerpFloat = 0;
        oldScale = gameObject.transform.localScale;
        if (isBig)
        {
            targetScale = bigScale;
        }
        else
        {
            targetScale = smallScale;
        }
    }
}
