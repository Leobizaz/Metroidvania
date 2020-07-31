using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lampada : MonoBehaviour
{
    public bool flickering;
    Animator anim;
    public GameObject redlight;
    public GameObject emissive;
    public GameObject emissiveRed;
    public GameObject normalLight;

    public bool startOn = false;

    private void Start()
    {
        emissiveRed.SetActive(true);
        redlight.SetActive(true);
        emissive.SetActive(false);
        normalLight.SetActive(false);
        anim = GetComponent<Animator>();
        anim.enabled = false;
        GameEvents.current.onReatorPowerUP += PowerUP;
        if (startOn) PowerUP();
    }

    void PowerUP()
    {
        emissiveRed.SetActive(false);
        emissive.SetActive(true);
        redlight.SetActive(false);
        normalLight.SetActive(true);
        anim.enabled = true;

    }

}
