using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float speed;
    public GameObject player;
    public GameObject sprite;

    float distance;
    Vector2 direction;
    bool looking;
    bool invLooking;

    private void Start()
    {
        currentHealth = maxHealth;
        looking = true;
    }

    private void Update()
    {
        SetHeading();
        LookAtPlayer();
        FollowPlayer();

    }


    void SetHeading()
    {
        var heading = player.transform.position - this.transform.position;
        distance = heading.magnitude;
        direction = heading / distance;
    }

    void LookAtPlayer()
    {
        if (looking)
        {
            if (direction.normalized.x > 0)
            {
                sprite.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.normalized.x < 0)
            {
                sprite.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (invLooking)
        {
            if (direction.normalized.x > 0)
            {
                sprite.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.normalized.x < 0)
            {
                sprite.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            Debug.Log("Block");
        }
    }


}
