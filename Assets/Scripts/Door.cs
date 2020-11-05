using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    private BoxCollider2D b_collider;
    AudioSource aSource;
    public AudioClip[] sfx;
    bool open;
    public bool Capitao;
    public GameObject capitaoRed;
    public GameObject capitaoGreen;
    public GameObject capitaoIndicator;

    private void Start()
    {
        b_collider = GetComponent<BoxCollider2D>();
        aSource = GetComponent<AudioSource>();

        if (Capitao)
        {
            capitaoGreen.SetActive(false);
            capitaoRed.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Capitao)
            {
                if (collision.GetComponent<PlayerController>().unlockedCartao)
                {
                    capitaoGreen.SetActive(true);
                    capitaoRed.SetActive(false);
                    CancelInvoke("DoorOpen");
                    Invoke("DoorOpen", 0.1f);
                }
                else
                {
                    capitaoIndicator.SetActive(true);
                    return;
                }
            }
            CancelInvoke("DoorOpen");
            Invoke("DoorOpen", 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            capitaoIndicator.SetActive(false);
            if (open)
            {
                CancelInvoke("DoorClose");
                Invoke("DoorClose", 0.1f);
            }

            if (Capitao)
            {
                capitaoGreen.SetActive(false);
                capitaoRed.SetActive(true);
            }

        }
    }

    public void DoorOpen()
    {
        anim.Play("open");
        open = true;
        aSource.PlayOneShot(sfx[0]);
        b_collider.enabled = false;
    }

    public void DoorClose()
    {
        open = false;
        anim.Play("close");
        aSource.PlayOneShot(sfx[1]);
        b_collider.enabled = true;
    }







}
