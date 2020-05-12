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


}
