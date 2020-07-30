using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableFade : MonoBehaviour
{
    Animator anim;
    public static ControllableFade current;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        current = this;
    }

    public void Fadeout(float time)
    {
        anim.Play("fadeout");
        if(time != 0)
        {
            CancelInvoke("Fadein");
            Invoke("Fadein", time);
        }
    }

    public void Fadein()
    {
        anim.Play("fadein");
    }



}
