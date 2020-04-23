using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSignal_Player : MonoBehaviour
{
    public SoundPlayer soundplayer;
    public AudioClip[] sons_de_passo;
    public AudioClip[] sons_de_mexendoMetal;

    public void SomDePasso()
    {
        soundplayer.PlayOneShotRandom(sons_de_passo);
    }

    public void SomDeMexendoSeiLa()
    {
        soundplayer.PlayOneShotRandom(sons_de_mexendoMetal);
    }


}
