using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fotoverme : MonoBehaviour
{
    SoundPlayer soundplayer;
    public int maxHP = 2;
    int currentHP;
    public bool upsideDown;
    bool fleeing;
    public bool detected;
    bool once;
    bool onLight;
    bool invLooking;
    public bool stunned;
    public GameObject blindFX;
    public bool blinded;
    public bool chasing;
    bool looking;
    public bool chargeCooldown;
    public GameObject cadaver;
    public float chargeForce = 1f;
    public PhysicsMaterial2D slippery;
    public PhysicsMaterial2D sticky;
    public ParticleSystem particleFX1;
    float timeOnLight;
    float timeOnLight2;
    float timeOutOfLight;
    public LayerMask layerMask;
    Rigidbody2D rb;
    public GameObject skin_viva;
    Animator viva_anim;
    Animator morto_anim;
    public GameObject skin_morta;
    public GameObject skin_charging;
    public GameObject player;
    GameObject skins;
    bool died;
    public float speed;
    float targetTime = 3;
    float distance;

    public AudioClip idleSound;

    public AudioClip[] atk_sounds;
    public AudioClip[] detectedsounds;
    public AudioClip[] hit_sounds;
    public AudioClip[] death_sounds;
    public AudioClip[] come_sounds;

    private void Awake()
    {
        Initialize();
        targetTime = 0.5f;
        //if (detected) GetDetected();
        player = GameObject.FindWithTag("Player");
    }

    private void OnEnable()
    {
        if (detected) Invoke("GetDetected", 0.1f);
    }

    void Update()
    {
        if(currentHP <= 0)
        {
            if (!died)
            {
                died = true;
                skin_charging.SetActive(false);
                skin_morta.SetActive(false);
                skin_viva.SetActive(true);
                viva_anim.Play("fotoverme_vivo_morrendo");
                soundplayer.PlayOneShotRandom(death_sounds);
                GameEvents.current.MakeSound(gameObject);
            }
            return;
        }
        if (onLight)
        {
            targetTime -= Time.deltaTime;
        }

        if (fleeing)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), -2.5f * speed * Time.deltaTime);
        }

        if (upsideDown)
        {
            FlipRaycast();
        }

        if (detected)
        {
            if(!blinded)
                ChasePlayer();
        }

        var heading = player.transform.position - this.transform.position;
        distance = heading.magnitude;
        Vector2 direction = heading / distance;

        if (looking)
        {
            if (direction.normalized.x > 0)
            {
                skins.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.normalized.x < 0)
            {
                skins.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (invLooking)
        {
            if (direction.normalized.x > 0)
            {
                skins.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.normalized.x < 0)
            {
                skins.transform.localScale = new Vector3(-1, 1, 1);
            }
        }




    }


    void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        viva_anim = skin_viva.GetComponent<Animator>();
        morto_anim = skin_morta.GetComponent<Animator>();
        skins = skin_viva.transform.parent.gameObject;
        looking = true;
        soundplayer = GetComponent<SoundPlayer>();
        currentHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Light")
        {
            if (targetTime <= 0)
            {
                if (!once)
                {
                    Grunt();
                    once = true;
                    Invoke("GetDetected", 1f);
                }
            }
            onLight = true;
        }

        if (collision.tag == "PlayerWeapon")
        {
            GetHit();
        }

        if (collision.tag == "Flash" && !blinded && !died)
        {
            blinded = true;
            Stunned();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Light")
        {
            onLight = true;
            if (targetTime <= 0)
            {
                if (!once)
                {
                    Grunt();
                    once = true;
                    Invoke("GetDetected", 1f);
                }
            }
        }

        if(collision.tag == "Flash" && !blinded && !died)
        {
            blinded = true;
            Stunned();
            CancelInvoke("BackToNormal");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Light")
        {
            onLight = false;
            targetTime = 0.5f;
        }
    }

    void FlipRaycast()
    {
        Vector3 offset = new Vector3(0, 1.0f, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position - offset, Vector2.up, 0.5f, layerMask);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Ground")
            {
                upsideDown = false;
                skin_viva.SetActive(true);
                skin_morta.SetActive(false);
                viva_anim.Play("fotoverme_vivo_agonizando");
                transform.localScale = new Vector3(1, 1, 1);
                //skins.transform.localScale = new Vector3(1, -1, 1);
            }
        }
    }

    public void GetDetected()
    {
        once = true;
        Debug.Log("Fuck");
        soundplayer.loop = false;
        soundplayer.GetComponent<AudioSource>().Stop();
        Grunt();
        particleFX1.Stop();
        Invoke("BreakStun", 4f);
        detected = true;
        chasing = true;

        rb.isKinematic = false;
        rb.WakeUp();
        //stunned = true;
        if (upsideDown)
        {
            skin_viva.SetActive(false);
            skin_morta.SetActive(true);
            return;
        }
        viva_anim.Play("fotoverme_vivo_agonizando");


    }

    void ChasePlayer()
    {

        if (chasing)
        {
            looking = true;
            if (Vector3.Dot(transform.up, Vector3.down) > 0.7f)
            {
                //Is upside down
                transform.DORotate(new Vector3(0, 0, 0), 0.5f);
            }

            viva_anim.Play("fotoverme_vivo_andando");

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);

            if(distance < 6 && !chargeCooldown)
            {
                chargeCooldown = true;
                chasing = false;
                PrepareCharge();
            }

        }



    }

    public void Congela()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
        CancelInvoke("StopCharging");
    }

    void PrepareCharge()
    {
        soundplayer.Stop();
        particleFX1.Play();
        rb.velocity = Vector3.zero;
        viva_anim.Play("fotoverme_vivo_preparando");
    }

    public void Charge()
    {
        //soundplayer.volume = 1;
        soundplayer.PlayOneShotRandom(atk_sounds);
        GameEvents.current.MakeSound(gameObject);
        looking = false;
        rb.mass = 0.5f;
        rb.sharedMaterial = slippery;

        skin_viva.SetActive(false);
        skin_charging.SetActive(true);
        Vector3 force = new Vector3(Mathf.Clamp(((player.transform.position.x - transform.position.x) * chargeForce), -6, 6), chargeForce, 0);
        Debug.Log("Launching with force: " + force);
        rb.AddForce(force, ForceMode2D.Impulse);
        Invoke("StopCharging", 1);
    }

    public void StopCharging()
    {
        soundplayer.Play(idleSound);
        particleFX1.Stop();
        //soundplayer.volume = 1;
        looking = true;
        rb.mass = 30;
        rb.sharedMaterial = sticky;
        skin_charging.SetActive(false);
        skin_viva.SetActive(true);
        chasing = true;
        Invoke("ResetChargeCooldown", 4f);
    }
    void ResetChargeCooldown()
    {
        chargeCooldown = false;
    }

    public void BreakStun()
    {
        chargeCooldown = false;
        soundplayer.Play(idleSound);
        soundplayer.loop = true;
        chasing = true;
        stunned = false;
    }

    public void Grunt()
    {
        GameEvents.current.MakeSound(gameObject);
        //soundplayer.volume = 0.2f;
        soundplayer.PlayOneShotRandom(detectedsounds);
    }

    public void GetHit()
    {
        soundplayer.PlayOneShotRandom(hit_sounds);
        currentHP--;
    }

    public void Die()
    {
        soundplayer.Stop();
        skin_viva.SetActive(false);
        skin_morta.SetActive(true);
        DOVirtual.Float(morto_anim.speed, 0, 3, UpdateAnimSpeed).SetEase(Ease.OutQuad);
        Invoke("Delete", 3.4f);
        GameEvents.current.MakeSound(gameObject);

    }

    public void Come()
    {
        soundplayer.PlayOneShotRandom(come_sounds);
        //GameEvents.current.MakeSound(gameObject);
    }

    void UpdateAnimSpeed(float x)
    {
        morto_anim.speed = x;
    }

    void Delete()
    {
        var heading = player.transform.position - this.transform.position;
        var distance = heading.magnitude;
        Vector2 direction = heading / distance;

        if (direction.normalized.x > 0)
        {
            var instance = Instantiate(cadaver, this.transform.position, this.transform.rotation);
            instance.transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
        }
        else
        {
            Instantiate(cadaver, this.transform.position, this.transform.rotation);
        }

        Destroy(this.gameObject);
    }

    void Stunned()
    {
        looking = false;
        rb.mass = 30;
        rb.sharedMaterial = sticky;
        skin_charging.SetActive(false);
        skin_viva.SetActive(true);
        viva_anim.Play("fotoverme_vivo_agonizando");
        blindFX.SetActive(true);
        Invoke("Flee", 1);
    }

    void Flee()
    {
        viva_anim.Play("fotoverme_vivo_andando");
        viva_anim.speed = 2;
        invLooking = true;
        fleeing = true;
        
        Invoke("BackToNormal", 3f);
    }

    void BackToNormal()
    {
        viva_anim.speed = 1;
        invLooking = false;
        fleeing = false;
        blinded = false;
        blindFX.SetActive(false);
        BreakStun();
    }
}
