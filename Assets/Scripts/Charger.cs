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
    public Animator anim;
    public GameObject clampPosition1;
    public GameObject clampPosition2;
    public float idleRange = 10;
    public ParticleSystem flashFX;
    public ParticleSystem explodeFX;
    public GameObject bolha;

    Vector3 idlePoint;

    public ParticleSystem FX_runDust;

    bool congelado;
    float timer;
    float distance;
    Vector2 direction;
    bool looking;
    bool invLooking;
    float oldDirection;
    bool playerInRange;

    public BoxCollider2D hitcol;
    Transform lastPlayerPosition;
    public AudioSource audio;
    public AudioClip[] sons;

    bool once;
    bool dead;
    bool charging;
    bool stunned;

    private void Start()
    {
        currentHealth = maxHealth;
        idlePoint = this.transform.position;
        looking = true;
        audio = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        RangeDetection();


        if (!congelado)
        {
            if (playerInRange)
            {
                Count();
                SetHeading();

                if (!stunned)
                {
                    if (!charging)
                    {
                        LookAtPlayer();
                        FollowPlayer();
                    }
                    else
                    {
                        Charge();
                    }
                }
            }
            else
            {
                FollowIdlePoint();
            }
        }

        //CLAMP
        if (!playerInRange) {
        transform.position =
            new Vector3(
                Mathf.Clamp(transform.position.x, clampPosition1.transform.position.x, clampPosition2.transform.position.x),
                transform.position.y,
                transform.position.z);
        }
    }



    void Count()
    {
        timer += Time.deltaTime;
        if(timer > 5f && !once)
        {
            once = true;
            PrepareCharge();
        }
    }

    public void PrepareCharge()
    {
        once = true;
        hitcol.enabled = false;
        anim.Play("charge");
        Invoke("StartCharging", 2f);
        stunned = true;
    }

    void RangeDetection()
    {
        if(Vector3.Distance(player.transform.position, idlePoint) > idleRange)
        {
            playerInRange = false;
        }
        else
        {
            if(Vector3.Distance(player.transform.position, idlePoint) < idleRange/2)
            playerInRange = true;
        }
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

    void FollowIdlePoint()
    {
        playerInRange = false;

        var heading = idlePoint - this.transform.position;
        distance = heading.magnitude;
        direction = heading / distance;

        if (direction.normalized.x > 0)
        {
            sprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.normalized.x < 0)
        {
            sprite.transform.localScale = new Vector3(1, 1, 1);
        }

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(idlePoint.x, transform.position.y), speed * Time.deltaTime);
    }

    void Charge()
    {
        Debug.Log("Cu");
        transform.position = Vector2.MoveTowards(transform.position, new Vector2((transform.position.x * oldDirection) * -20, transform.position.y), (speed * 3) * Time.deltaTime);

    }

    void Stunned()
    {

    }

    public void StartCharging()
    {
        Debug.Log("Start charging");
        hitcol.enabled = true;
        lastPlayerPosition = player.transform;
        oldDirection = direction.x;
        stunned = false;

        if (direction.normalized.x > 0)
        {
            sprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.normalized.x < 0)
        {
            sprite.transform.localScale = new Vector3(1, 1, 1);
        }

        anim.Play("run");
        anim.Play("run_arm");
        charging = true;
        FX_runDust.Play();
        Invoke("Tired", 3f);
    }
    
    public void Tired()
    {
        hitcol.enabled = false;
        gameObject.layer = 13;
        FX_runDust.Stop();
        anim.Play("empty");
        anim.Play("tired");
        Debug.Log("Tired");
        charging = false;
        stunned = true;
        Invoke("GetBackUP", 3.5f);
    }

    public void GetBackUP()
    {
        hitcol.enabled = true;
        gameObject.layer = 16;
        stunned = false;
        timer = 0;
        once = false;
        anim.Play("walk");
        anim.Play("walk_arm");
    }

    public void Congela()
    {
        CancelInvoke("GetBackUP");
        CancelInvoke("Tired");
        CancelInvoke("StartCharging");
        CancelInvoke("FollowIdlePoint");
        anim.speed = 0;
        congelado = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dead)
        {
            if (collision.gameObject.tag == "PlayerWeapon" && !charging)
            {

                Debug.Log("Block");
                stunned = true;
                hitcol.enabled = false;
                anim.Play("block");
                anim.Play("empty");
            }

            if (collision.gameObject.tag == "Flash")
            {
                CancelInvoke("GetBackUP");
                CancelInvoke("Tired");
                CancelInvoke("StartCharging");
                CancelInvoke("FollowIdlePoint");
                stunned = true;
                hitcol.enabled = false;
                FX_runDust.Stop();
                flashFX.Play();
                anim.Play("empty");
                anim.Play("chargerflash");
            }

            if (collision.gameObject.tag == "clamp")
            {
                CancelInvoke("GetBackUP");
                CancelInvoke("Tired");
                CancelInvoke("StartCharging");
                CancelInvoke("FollowIdlePoint");
                anim.Play("Tired");
                anim.Play("empty");
                Invoke("FollowIdlePoint", 2);
            }
        }
    }

    public void Thump()
    {
        GameEvents.current.ShakeCamera(0.3f, 0.5f, 1.5f);
    }

    public void PlayerHIT()
    {
        Debug.Log("Player HIT");
        player.GetComponent<PlayerController>().GetHitSpecific(this.gameObject);
        GameEvents.current.ShakeCamera(0.1f, 1.3f, 2.5f);
        anim.Play("hit_arm");
        CancelInvoke("Tired");
        Invoke("Tired", 1f);
    }

    public void Explode()
    {
        CancelInvoke("GetBackUP");
        CancelInvoke("Tired");
        CancelInvoke("StartCharging");
        CancelInvoke("FollowIdlePoint");
        dead = true;
        hitcol.enabled = false;
        gameObject.layer = 13;
        explodeFX.Play();
        bolha.SetActive(false);
        Debug.Log("Explode");
        anim.Play("explode");
    }

    public void Dead()
    {
        Congela();
    }

}
