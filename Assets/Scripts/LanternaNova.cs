using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternaNova : MonoBehaviour
{
    Animator anim;
    public PlayerLanternController lanternController;
    public AudioClip[] soundtrack;
    AudioSource audio;

    bool on;
    float cooldown;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.Play("lanterna_open");
        audio.clip = soundtrack[1];
        audio.Play();
    }

    private void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z) && cooldown <= 0)
        {
            if (on)
            {
                anim.Play("lanterna_close");
                audio.clip = soundtrack[0];
                audio.Play();
            }
            else
            {
                anim.Play("lanterna_open");
                audio.clip = soundtrack[1];
                audio.Play();
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
