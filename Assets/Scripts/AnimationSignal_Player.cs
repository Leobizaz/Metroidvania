using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSignal_Player : MonoBehaviour
{
    public SoundPlayer soundplayer;
    public bool metal;
    public AudioClip[] sons_de_passo;
    public AudioClip[] sons_de_passo_metal;
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
        soundplayer.PlayOneShotRandom(sons_de_passo);
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


}
