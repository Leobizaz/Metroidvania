using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public ParticleSystem fx;
    AudioSource aSource;
    bool once;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "chonk" && !once)
        {
            once = true;
            fx.Play();
            GameEvents.current.ShakeCamera(0.3f, 2, 1);
            Invoke("ResetOnce", 3f);
        }
    }

    void ResetOnce()
    {
        once = false;
    }
}
