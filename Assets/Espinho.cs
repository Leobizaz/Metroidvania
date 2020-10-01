using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinho : MonoBehaviour
{
    bool once;
    public GameObject espinhoSprite;
    AudioSource audioS;
    public ParticleSystem espinhobreakFX;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !once)
        {
            once = true;
            espinhoSprite.SetActive(false);
            audioS.Play();
            espinhobreakFX.Play();
            collision.gameObject.GetComponent<PlayerController>().GetHitSpecific(this.gameObject);
        }
    }

}
