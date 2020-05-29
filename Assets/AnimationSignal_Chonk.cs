using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSignal_Chonk : MonoBehaviour
{
    public AudioClip[] audioclips;
    AudioSource aSource;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void SmallShake()
    {
        GameEvents.current.ShakeCamera(1, 1, 1);
    }

    public void BigShake()
    {
        GameEvents.current.ShakeCamera(2, 2, 2);
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

    }
}
