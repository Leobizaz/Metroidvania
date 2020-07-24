using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSyncAnimation : MonoBehaviour
{
    public float overRide = 0;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();


        if(overRide != 0)
        {
            anim.Play(0, -1, overRide);
        }
        else
        {
            anim.Play(0, -1, Random.value);
        }
    }



}
