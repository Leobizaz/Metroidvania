using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource asource;
    public bool loop;
    public float volume;

    private void Start()
    {
        asource = GetComponent<AudioSource>();
        loop = asource.loop;
        volume = asource.volume;
    }

    private void Update()
    {
        asource.loop = loop;
        asource.volume = volume;
    }

    public void Play(AudioClip audio)
    {
        asource.clip = audio;
        asource.Play();
    }

    public void Stop()
    {
        asource.Stop();
    }

    public void PlayOneShot(AudioClip audio)
    {
        asource.PlayOneShot(audio);
    }

    public void PlayOneShotRandom(AudioClip[] audios)
    {
        int i = Random.Range(0, audios.Length);
        asource.PlayOneShot(audios[i]);

    }
}
