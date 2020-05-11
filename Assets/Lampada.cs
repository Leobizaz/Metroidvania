using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lampada : MonoBehaviour
{
    public bool flickering;
    Animator anim;
    public GameObject redlight;
    public GameObject emissive;
    public GameObject normalLight;

    private void Start()
    {
        redlight.SetActive(true);
        emissive.SetActive(false);
        normalLight.SetActive(false);
        anim = GetComponent<Animator>();
        anim.enabled = false;
        GameEvents.current.onReatorPowerUP += PowerUP;
    }

    void PowerUP()
    {
        emissive.SetActive(true);
        redlight.SetActive(false);
        normalLight.SetActive(true);
        anim.enabled = true;

    }

}
