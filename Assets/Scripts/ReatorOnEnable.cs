using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReatorOnEnable : MonoBehaviour
{
    AudioSource audioS;
    public GameObject FX;
    public GameObject subFX;
    public GameObject monitores;
    public GameObject powerUP_event;

    private void OnEnable()
    {
        //ReatorPowerUP();
        //sfx

        Invoke("ReactorLightUP", 1);
    }

    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }
    void Update()
    {
        
    }

    void ReatorPowerUP()
    {
        audioS.Stop();
        GameEvents.current.ReatorPowerUP();
        CameraShake.current.ShakeCamera(2, 0.3f, 1);
        powerUP_event.SetActive(true);
        FX.SetActive(true);
        Invoke("LessShake", 2);
    }

    void LessShake()
    {
        CameraShake.current.ShakeCamera(2, 0.1f, 1);
    }

    void ReactorLightUP()
    {
        //sfx
        audioS.Play();
        monitores.SetActive(true);
        Invoke("Shake", 2f);
    }

    void Shake()
    {
        subFX.SetActive(true);
        CameraShake.current.ShakeCamera(4, 0.5f, 2);
        Invoke("ReatorPowerUP", 4);
    }

}
