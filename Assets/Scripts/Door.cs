using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    private BoxCollider2D b_collider;
    AudioSource aSource;
    public AudioClip[] sfx;


    private void Start()
    {
        b_collider = GetComponent<BoxCollider2D>();
        aSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CancelInvoke("DoorOpen");
            Invoke("DoorOpen", 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CancelInvoke("DoorClose");
            Invoke("DoorClose", 0.1f);
        }
    }

    public void DoorOpen()
    {
        anim.Play("open");
        aSource.PlayOneShot(sfx[0]);
        b_collider.enabled = false;
    }

    public void DoorClose()
    {
        anim.Play("close");
        aSource.PlayOneShot(sfx[1]);
        b_collider.enabled = true;
    }







}
