using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnimationEvents : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip[] sonsRugido;

    public void Scream()
    {
        audioS.clip = sonsRugido[Random.Range(0, sonsRugido.Length)];
        audioS.Play();


        GameEvents.current.ShakeCamera(1.5f, 1.3f, 1.5f);
    }
}
