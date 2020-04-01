using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHazard : MonoBehaviour
{
    public Sprite catchSprite;


    /////////////// Fazer Sistema de Dano ao player/////////////////
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = catchSprite; //Remover isso qundo ter a animação final
        }
    }

}
