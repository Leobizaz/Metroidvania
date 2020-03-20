using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    SpriteRenderer spr_renderer;
    bool isMoving;
    Rigidbody2D rb;
    Animator anim;
    bool once;

    public GameObject playercamera;

    public float speed;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Character");
        spr_renderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (isMoving)
        {

            var heading = player.transform.position - this.transform.position;
            var distance = heading.magnitude;
            Vector2 direction = heading / distance;

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);

            if(direction.normalized.x > 0)
            {
                spr_renderer.flipX = true;
            }
            else if(direction.normalized.x < 0)
            {
                spr_renderer.flipX = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Light" && !once)
        {
            once = true;
            playercamera.GetComponent<CinemachineVirtualCamera>().m_Priority = 0;
            anim.Play("Transform_RICARDO");
        }

        if(collision.tag == "PlayerWeapon")
        {
            anim.Play("RICARDO_HIT");
        }
    }

    public void TurnOnMovement()
    {
        
        rb.isKinematic = false;

        isMoving = true;
    }


}
