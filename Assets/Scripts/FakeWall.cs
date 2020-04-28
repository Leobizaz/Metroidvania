using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FakeWall : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sprite;
    BoxCollider2D b_collider;
    public ParticleSystem fx;
    public HideSecret hideSecret;
    public bool isTrigger = false;
    public float fadeTime = 3;
    public bool doBreakFX = false;
    bool once;

    private void Awake()
    {
        b_collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Break()
    {
        if (doBreakFX)
            anim.Play("Break");
        else SpawnParticles();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && isTrigger && !once)
        {
            once = true;
            Break();
        }
    }

    public void SpawnParticles()
    {
        if(hideSecret != null)
        {
            hideSecret.time = fadeTime;
            hideSecret.Unhide();
        }

        if (doBreakFX)
        {
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
