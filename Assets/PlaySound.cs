using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    AudioSource aSource;
    public float delay;
    public bool onEnable;
    public bool oneTime = true;
    bool once;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if(onEnable && !once)
            Invoke("Play", delay);
    }

    public void Play()
    {
        if (oneTime) once = true;
        aSource.Play();
    }


}
