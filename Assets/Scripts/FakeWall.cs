using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FakeWall : MonoBehaviour
{
    Animator anim;
    public SpriteRenderer sprite;
    BoxCollider2D b_collider;
    AudioSource aSource;
    public ParticleSystem fx;
    public HideSecret hideSecret;
    public bool isTrigger = false;
    public float fadeTime = 3;
    public bool doBreakFX = false;
    bool once;
    public bool chaseEvent;


    private void Awake()
    {
        if(chaseEvent)
            GameEvents.current.onChaseReset += OnReset;
        b_collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        if (GetComponent<AudioSource>())
        {
            aSource = GetComponent<AudioSource>();
        }
    }

    void OnReset()
    {
        once = false;
        sprite.gameObject.SetActive(true);
        sprite.enabled = true;
        b_collider.enabled = true;
    }

    public void Break()
    {
        if (!once)
        {
            if (doBreakFX)
            {
                aSource.Play();
                anim.Play("Break");
            }
            else SpawnParticles();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && isTrigger && !once)
        {
            once = true;
            Break();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && isTrigger && !once)
        {
            once = true;
            Break();
        }
    }

    public void SpawnParticles()
    {
        if (!once)
        {
            once = true;
            if (hideSecret != null)
            {
                hideSecret.time = fadeTime;
                hideSecret.Unhide();
            }

            if (doBreakFX)
            {
                GameEvents.current.MakeBigSound(gameObject);
                sprite.enabled = false;
                fx.Play();
            }
            else
            {
                sprite.DOColor(new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0), fadeTime);
            }
            b_collider.enabled = false;

        }
    }
}
