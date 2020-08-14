using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSignal_Player : MonoBehaviour
{
    public SoundPlayer soundplayer;
    public bool metal;
    public bool carne;
    public bool pedra;
    public AudioClip[] sons_de_passo;
    public AudioClip[] sons_de_passo_metal;
    public AudioClip[] sons_de_passo_carne;
    public AudioClip[] sons_de_mexendoMetal;
    PlayerController player;

    private void Awake()
    {
        player = transform.parent.GetComponent<PlayerController>();
    }

    public void SomDePasso()
    {
        if (metal)
        {
            soundplayer.PlayOneShotRandom(sons_de_passo_metal);
            return;
        }
        else if (pedra)
        {
            soundplayer.PlayOneShotRandom(sons_de_passo);
            return;
        }
        else if (carne)
        {
            soundplayer.PlayOneShotRandom(sons_de_passo_carne);
            return;
        }
        else
        {
            soundplayer.PlayOneShotRandom(sons_de_passo);
        }
    }

    public void SomDeMexendoSeiLa()
    {
        soundplayer.PlayOneShotRandom(sons_de_mexendoMetal);
    }

    public void PushButton()
    {
        //sfx botão
    }

    public void NotBusy()
    {
        player.isBusy = false;
    }

    public void PassoMetal()
    {
        metal = true;
        pedra = false;
        carne = false;
    }

    public void PassoPedra()
    {
        metal = false;
        pedra = true;
        carne = false;
    }

    public void PassoCarne()
    {
        carne = true;
        metal = false;
        pedra = false;
    }

}
