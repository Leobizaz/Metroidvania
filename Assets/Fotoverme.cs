using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fotoverme : MonoBehaviour
{
    public bool upsideDown;
    bool detected;
    bool once;
    bool onLight;
    bool stunned;
    float timeOnLight;
    float timeOutOfLight;
    public LayerMask layerMask;
    Rigidbody2D rb;
    public GameObject skin_viva;
    Animator viva_anim;
    public GameObject skin_morta;
    public GameObject skin_charging;
    public GameObject player;
    GameObject skins;
    public float speed;
    private void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (upsideDown)
        {
            FlipRaycast();
        }

        if (detected && stunned)
        {
            if (onLight)
            {
                timeOutOfLight = 0;
                timeOnLight = 1 + Time.time;
            }
            else
            {
                timeOutOfLight = Time.time;
                if(timeOutOfLight > timeOnLight)
                {
                    stunned = false;
                    CancelInvoke("BreakStun");
                }
            }
        }
        else if (detected && !stunned)
        {
            ChasePlayer();
        }




    }

    void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        viva_anim = skin_viva.GetComponent<Animator>();
        skins = skin_viva.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Light")
        {
            if(!once) GetDetected();
            onLight = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Light")
        {
            onLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Light")
        {
            onLight = false;
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
            }
        }
    }

    public void GetDetected()
    {
        Invoke("BreakStun", 7f);
        once = true;
        detected = true;
        rb.isKinematic = false;
        rb.WakeUp();
        stunned = true;
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
        viva_anim.Play("fotoverme_vivo_andando");
        Debug.Log("I'm chasing you");

        var heading = player.transform.position - this.transform.position;
        var distance = heading.magnitude;
        Vector2 direction = heading / distance;

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);

        if (direction.normalized.x > 0)
        {
            skins.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.normalized.x < 0)
        {
            skins.transform.localScale = new Vector3(1, 1, 1);
        }


    }

    public void BreakStun()
    {
        stunned = false;
    }
}
