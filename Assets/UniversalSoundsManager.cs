using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalSoundsManager : MonoBehaviour
{
    AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void PlaySpookSound()
    {
        audioS.Play();
    }
}
