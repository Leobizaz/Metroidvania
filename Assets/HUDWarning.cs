using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDWarning : MonoBehaviour
{
    AudioSource audioS;
    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void PlayBeep()
    {
        audioS.Play();
    }
}
