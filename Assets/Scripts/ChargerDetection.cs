using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerDetection : MonoBehaviour
{
    bool once;
    public Charger charger;
    public Transform explosionPoint;
    float cooldown;
    public float distance = 10;
    PlayerController player;

    private void Start()
    {
        explosionPoint = charger.transform;
    }

    private void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && cooldown <= 0)
        {
            var dir = (collision.gameObject.transform.position - transform.position);
            cooldown = 2f;
            charger.PlayerHIT();
            player = collision.gameObject.GetComponent<PlayerController>();
            //player.isBusy = true;
            ExplosionForce(collision);
            Invoke("Aaa", 0.3f);
        }
    }

    void Aaa()
    {
        player.isBusy = false;
    }


    void ExplosionForce(Collider2D players)
    {
            Rigidbody2D rb = players.GetComponent<Rigidbody2D>();

        var dir = explosionPoint.position - players.transform.position;

        var x = dir.normalized.x * distance;
        var y = 5;
        
        rb.AddForce(new Vector2(-x, y), ForceMode2D.Impulse);
        Debug.Log("Launching " + rb.gameObject.name +  " at " + new Vector2(x, y));


    }

}
