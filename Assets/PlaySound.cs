using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    AudioSource aSource;
    public float delay;
    public bool onEnable;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if(onEnable)
            Invoke("Play", delay);
    }

    public void Play()
    {
        aSource.Play();
    }


}
