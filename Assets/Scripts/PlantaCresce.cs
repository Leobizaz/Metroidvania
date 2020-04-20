using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaCresce : MonoBehaviour
{
    public Animator anim;
    bool grown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Light" && !grown)
        {
            Debug.Log("Light");
            anim.Play("Wiggle");
        }

        if(collision.tag == "Flash" && !grown)
        {
            anim.Play("Grow");
            grown = true;
        }
    }
}
