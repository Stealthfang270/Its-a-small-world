using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public AudioSource startSrc;
    public AudioSource stopSrc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !startSrc.isPlaying)
        {
            startSrc.Play();
            stopSrc.Stop();
        }
    }
}
