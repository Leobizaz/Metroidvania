using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerBolha : MonoBehaviour
{
    public Charger charger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            charger.Explode();
            gameObject.SetActive(false);
        }
    }
}
