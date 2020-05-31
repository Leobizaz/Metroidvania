using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationSignal_Chonk : MonoBehaviour
{
    public AudioClip[] audioclips;
    AudioSource aSource;
    public QuebraParede chaoInfect;
    public QuebraParede barreira;
    public ChonkChase chonk;


    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }
    public void QuebraPorta()
    {
        barreira.Quebra();
    }

    public void SmallShake()
    {
        GameEvents.current.ShakeCamera(1, 1, 1);
    }

    public void BigShake()
    {
        GameEvents.current.ShakeCamera(2, 10, 10);
    }

    public void Rugido()
    {
        aSource.PlayOneShot(audioclips[0]);
    }

    public void MiniRugido()
    {
        aSource.PlayOneShot(audioclips[1]);
    }

    public void QuebraChão()
    {
        chaoInfect.Quebra();
    }

    public void IgnoreSpeedTRUE()
    {
        chonk.ignoreSpeed = true;
        DOVirtual.Float(chonk.anim.speed, 1.5f, 1f, ChangeSpeed);
    }

    public void IgnoreSpeedFALSE()
    {
        chonk.ignoreSpeed = false;
    }

    public void IgnoreSlowTRUE()
    {
        chonk.ignoreSpeed = true;
        DOVirtual.Float(chonk.anim.speed, 0.3f, 0.5f, ChangeSpeed);
    }

    public void ChangeSpeed(float s)
    {
        chonk.anim.speed = s;
    }
}
