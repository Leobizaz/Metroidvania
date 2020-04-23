using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estalactite : MonoBehaviour
{
    /////////////// Fazer Sistema de Dano ao player/////////////////


    public bool trap = false;
    bool once;
    private Rigidbody2D rb;
    AudioSource audioS;
    public ParticleSystem FX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioS = GetComponent<AudioSource>();

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && trap)
        {
            this.rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // tira vida
        }
        if(other.gameObject.tag == "Ground" && !once)
        {
            once = true;
            FX.Play();
        }
        if (other.gameObject.tag == "Enemy")
        {
            // mata/tira vida
        }
    }

}
