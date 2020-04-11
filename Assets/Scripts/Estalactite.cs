using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estalactite : MonoBehaviour
{
    /////////////// Fazer Sistema de Dano ao player/////////////////


    public bool trap = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (other.gameObject.tag == "Enemy")
        {
            // mata/tira vida
        }
    }

}
