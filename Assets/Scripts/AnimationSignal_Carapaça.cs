using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSignal_Carapaça : MonoBehaviour
{
    AudioSource aSource;
    public AudioClip[] sfx;
    public ParticleSystem fx_ground;
    [SerializeField] bool onScreen;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();

    }

    public void Step()
    {
        aSource.pitch = Random.Range(1, 0.5f);

        if(onScreen)
            CameraShake.current.ShakeCamera(0.1f, 0.3f, 1.5f);

        aSource.PlayOneShot(sfx[0]);
    }

    private void OnBecameVisible()
    {
        Debug.Log("Visivel");
        onScreen = true;
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Invisivel");
        onScreen = false;
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

