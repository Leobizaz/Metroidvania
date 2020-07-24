using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomSpeed : MonoBehaviour
{
    public float overRide = 0;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if(overRide != 0)
        {
            anim.speed = overRide;
        }
        else
        {
            anim.speed = Random.value;
        }

    }


}
