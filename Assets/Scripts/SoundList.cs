using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : MonoBehaviour
{
    public AudioClip[] soundtrack;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (!audio.playOnAwake)
        {
            audio.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            audio.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audio.Play();
        }
    }
}
