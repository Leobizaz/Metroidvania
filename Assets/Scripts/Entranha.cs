using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entranha : MonoBehaviour
{
    public Animator spriteAnim;
    public GameObject sprite_cortado;
    public GameObject collision;
    public bool cortado;
    public bool explosive;
    public GameObject spore;
    public ParticleSystem explosion;

    public GameObject[] disableOnCut;
    public GameObject[] enableOnCut;

    public AudioSource audio;
    public AudioClip[] sonsDano;

    private void Start()
    {
        if (cortado)
        {
            Corta();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerWeapon")
        {
            if (!cortado)
            {
                if (!explosive)
                {
                    if (collision.name == "fx_SlashSmall" || collision.name == "fx_SlashBig")
                    {
                        Corta();
                    }
                    else
                    {
                        if (!cortado)
                            GetHit();
                    }
                }
                else
                {
                    if (collision.name != "fx_SlashSmall" && collision.name != "fx_SlashBig")
                    {
                        Corta();
                        spore.SetActive(false);
                        explosion.Play();

                    }
                    else
                    {
                        if (!cortado) GetHit();
                    }
                }
            }
        }
    }

    public void GetHit()
    {
        //audio.clip = sonsDano[Random.Range(0, sonsDano.Length)];
        //audio.Play();
        spriteAnim.Play("entranhas_hit");
    }

    public void Corta()
    {
        cortado = true;
        spriteAnim.Play("entranhas_cut");
        collision.SetActive(false);
        sprite_cortado.SetActive(true);

        if (disableOnCut != null)
        {
            foreach(GameObject obj in disableOnCut)
            {
                obj.SetActive(false);
            }
        }

        if(enableOnCut != null)
        {
            foreach(GameObject obj in enableOnCut)
            {
                obj.SetActive(true);
            }
        }

    }

}
