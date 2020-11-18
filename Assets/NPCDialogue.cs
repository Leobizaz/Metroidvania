using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public Animator dialogwindow;

    private void Start()
    {
        dialogwindow.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            dialogwindow.gameObject.SetActive(true);
            dialogwindow.Play("Open");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            dialogwindow.Play("Close");
        }
    }
}
