using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChase : MonoBehaviour
{
    bool once;
    public AudioSource TerminaMusica;
    public AudioSource somTermina;
    public GameObject fadeOut;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !once)
        {
            once = true;
            End();
        }
    }

    public void End()
    {
        Invoke("EndMusica", 0.4f);
        somTermina.Play();
        Invoke("FadeOut", 5);
    }

    void EndMusica()
    {
        TerminaMusica.Stop();
    }

    public void FadeOut()
    {
        fadeOut.SetActive(true);
    }

}


