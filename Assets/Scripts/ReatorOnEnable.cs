using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReatorOnEnable : MonoBehaviour
{
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
        
    }
    void Update()
    {
        
    }

    void ReatorPowerUP()
    {
        GameEvents.current.ReatorPowerUP();
        CameraShake.current.ShakeCamera(2, 0.3f, 1);
        powerUP_event.SetActive(true);
        FX.SetActive(true);
    }

    void ReactorLightUP()
    {
        //sfx
        monitores.SetActive(true);
        Invoke("Shake", 2f);
    }

    void Shake()
    {
        subFX.SetActive(true);
        CameraShake.current.ShakeCamera(2, 0.5f, 2);
        Invoke("ReatorPowerUP", 2);
    }

}
