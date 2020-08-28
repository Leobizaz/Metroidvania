using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternaNova : MonoBehaviour
{
    Animator anim;
    public PlayerLanternController lanternController;

    bool on;
    float cooldown;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("lanterna_open");
    }

    private void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z) && cooldown <= 0)
        {
            if (on)
            {
                anim.Play("lanterna_close");
            }
            else
            {
                anim.Play("lanterna_open");
            }
            cooldown = 1.5f;
            on = !on;
        }
    }

    public void LightON()
    {
        lanternController.LightON();
    }

    public void LightOFF()
    {
        lanternController.LightOFF();
    }


}
