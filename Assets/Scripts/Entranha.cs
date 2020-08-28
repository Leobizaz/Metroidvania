using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entranha : MonoBehaviour
{
    public Animator spriteAnim;
    public GameObject sprite_cortado;
    public GameObject collision;
    public bool cortado;

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


            if(collision.name == "fx_SlashSmall" || collision.name == "fx_SlashBig")
            {
                Corta();
            }
            else
            {
                GetHit();
            }
        }
    }

    public void GetHit()
    {
        spriteAnim.Play("entranhas_hit");
    }

    public void Corta()
    {
        spriteAnim.Play("entranhas_cut");
        collision.SetActive(false);
        sprite_cortado.SetActive(true);
    }

}
