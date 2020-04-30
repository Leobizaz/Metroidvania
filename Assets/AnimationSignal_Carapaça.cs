using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSignal_Carapaça : MonoBehaviour
{
    AudioSource aSource;
    public AudioClip[] sfx;
    public ParticleSystem fx_ground;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();

    }

    public void Step()
    {
        aSource.pitch = Random.Range(1, 0.5f);
        aSource.PlayOneShot(sfx[0]);
    }

    public void FX_Ground()
    {
        fx_ground.Play();
    }

    public void AttackSfx()
    {
        aSource.pitch = 1;
        aSource.PlayOneShot(sfx[1]);
    }
}

