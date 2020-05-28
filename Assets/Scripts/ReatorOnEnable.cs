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
    public PlayerController player;
    bool once;

    private void OnEnable()
    {
        if (GameLoad.playerHasDiedOnce)
        {
            ForceTurnOn();
            return;
        }


        if (!once)
        {
            player.transform.position = new Vector3(-28.899f, -21.266f, 0);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            player.Freio();

            if (!PlayerController.facingleft)
                player.FaceLeft();

            player.isBusy = true;
            player.gameObject.GetComponentInChildren<Animator>().Play("Bob_InteractConsole");
            player.Freio();
            once = true;
            Invoke("ReactorLightUP", 1);
        }
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

    public void ForceTurnOn()
    {
        GameEvents.current.ReatorPowerUP();
        powerUP_event.SetActive(true);
        monitores.SetActive(true);
        subFX.SetActive(true);
        FX.SetActive(true);

    }

}
