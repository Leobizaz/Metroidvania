using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantashield : MonoBehaviour
{
    public Transform cabess;
    public GameObject particles;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerWeapon")
        {


            if (collision.name == "fx_SlashSmall" || collision.name == "fx_SlashBig")
            {
                if(cabess == null)
                Instantiate(particles, transform.position, Quaternion.identity);
                else
                {
                    Instantiate(particles, cabess.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
}
