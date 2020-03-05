using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : MonoBehaviour
{

    public GameObject player;
    bool isMoving;
    bool isAttacking;
    public float speed = 5f;
    public Animator anim;
    bool safezone;

    void Start()
    {
        isMoving = true;
    }

    private void OnEnable()
    {
        speed = 5f;
    }

    private void Update()
    {
        var heading = player.transform.position - this.transform.position;
        var distance = heading.magnitude;

        if (isMoving && !isAttacking)
        {
            anim.Play("spider_moving");
            Vector2 direction = heading / distance;

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);

            if (direction.normalized.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.normalized.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if(distance < 1 && !isAttacking)
        {
            isAttacking = true;
            anim.Play("spider_attacking");
            Invoke("StopAttacking", 1f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Safezone" && !safezone)
        {
            Debug.Log("Hit");
            safezone = true;
            isMoving = false;
            anim.Play("spider_still");
            Invoke("RunAway", 2f);
        }
    }

    void StopAttacking()
    {
        isAttacking = false;
    }

    void RunAway()
    {
        isMoving = true;
        speed = -speed;
        Invoke("Disable", 3f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
