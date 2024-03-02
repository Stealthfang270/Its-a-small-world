using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.toggle.AddListener(OnToggle);
    }

    void OnToggle()
    {
        gameObject.GetComponent<Collider2D>().enabled = !gameObject.GetComponent<Collider2D>().enabled;
    }
}
