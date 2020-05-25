using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : MonoBehaviour
{
    int index;
    [SerializeField] GameObject currentTarget;
    GameObject lastTargetObj;
    [SerializeField] private Vector3 lastTargetPosition;
    AudioSource aSource;
    public Animator anim;
    public AudioClip[] sfx;
    public float range = 20;
    public float speed = 5;
    float currentspeed;
    [SerializeField] bool moving;
    bool focusedListening;
    bool movingFast;
    [SerializeField] float giveUpTimer;
    public GameObject sprite;
    [SerializeField] float distance;
    bool looking;
    public GameObject head;
    public LayerMask whatIsGround;
    public ParticleSystem bumpFX;
    [SerializeField] bool block;
    float atkCooldown;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        GameEvents.current.onMakeSound += OnListenSound;
        GameEvents.current.onMakeBigSound += OnListenBigSound;
        currentspeed = speed;
    }

    void Update()
    {
        if (atkCooldown > 0) atkCooldown -= Time.deltaTime;

        if (looking)
        {
            var heading = lastTargetPosition - this.transform.position;
            distance = heading.magnitude;
            Vector2 direction = heading / distance;

            if (direction.normalized.x > 0)
            {
                sprite.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.normalized.x < 0)
            {
                sprite.transform.localScale = new Vector3(1, 1, 1);
            }

        }


        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(lastTargetPosition.x, transform.position.y), currentspeed * Time.deltaTime);
            float dist = this.transform.position.x - lastTargetPosition.x;

            if (Physics2D.OverlapCircle(head.transform.position, 0.2f, whatIsGround))
            {
                if (movingFast)
                {
                    Bump();
                    return;
                }
                else
                {
                    StopMoving();
                }
            }

            if (Mathf.Abs(dist) <= 0.4f && atkCooldown <= 0)
            {
                StopMoving();
                Attack();
            }

            giveUpTimer -= Time.deltaTime;
            if (giveUpTimer <= 0)
            {
                StopMoving();
            }


        }
    }

    private void OnListenSound(GameObject obj)
    {
        


        if (focusedListening)
        {
            float dist = this.transform.position.x - obj.transform.position.x;
            if (dist <= range / 4f)
            {
                if(obj == lastTargetObj)
                {
                    index++;
                    CancelInvoke("StopFocusedListening");
                    lastTargetObj = currentTarget;
                    currentTarget = obj;
                    lastTargetPosition = currentTarget.transform.position;
                    giveUpTimer = 8;
                    Invoke("StopFocusedListening", 2);
                    if(index > 3)
                    {
                        anim.Play("run");
                        currentspeed = speed * 2.5f;
                        anim.speed = 2;
                    }
                }
                else
                {
                    index = 0;
                }
            }
            else
            {
                index = 0;
            }
        }

        if (!moving && !block)
        {
            float dist = this.transform.position.x - obj.transform.position.x;
            if (dist <= range/4f)
            {
                if(currentTarget!= null)
                    lastTargetObj = currentTarget;

                focusedListening = true;
                Invoke("StopFocusedListening", 2);
                anim.Play("run");
                anim.speed = 1;
                currentspeed = speed;
                looking = true;
                aSource.PlayOneShot(sfx[0]);
                MoveToward();
                currentTarget = obj;
                lastTargetPosition = currentTarget.transform.position;
                giveUpTimer = 8;
            }
        }
    }

    void StopFocusedListening()
    {
        focusedListening = false;
    }

    private void OnListenBigSound(GameObject obj)
    {
        if (!block)
        {
            float dist = this.transform.position.x - obj.transform.position.x;
            if (dist <= range)
            {
                anim.Play("run");
                currentTarget = obj;
                lastTargetPosition = currentTarget.transform.position;
                giveUpTimer = 14;
                aSource.PlayOneShot(sfx[1]);
                looking = true;
                moving = true;
                movingFast = true;
                currentspeed = speed * 2;
                anim.speed = 2;
            }
        }
    }

    void Bump()
    {
        aSource.PlayOneShot(sfx[2]);
        StopMoving();
        anim.Play("bump");
        bumpFX.Play();
        Debug.Log("Hit a wall");
        block = true;
        Invoke("Unblock", 1.8f);
    }

    void Attack()
    {
        atkCooldown = 2f;
        index = 0;
        anim.speed = 1;
        anim.Play("attack");
        //aSource.PlayOneShot(sfx[3]);
        block = true;
        Invoke("Unblock", 0.5f);
    }

    void Unblock()
    {
        block = false;
    }
    
    void StopMoving()
    {
        movingFast = false;
        anim.Play("idle");
        moving = false;
        looking = false;
    }

    void MoveToward()
    {
        moving = true;
    }
}
