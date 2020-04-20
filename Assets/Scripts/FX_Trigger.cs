using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Trigger : MonoBehaviour
{
    public ParticleSystem[] particles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Stop();
        }
    }





    public void Play()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }

    public void Stop()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
        }
    }
}


