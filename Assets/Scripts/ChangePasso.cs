using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePasso : MonoBehaviour
{
    public AnimationSignal_Player bob_sprite;

    public bool Metal;
    public bool Pedra;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (Pedra)
            {
                bob_sprite.metal = false;
            }
            if (Metal)
            {
                bob_sprite.metal = true;
            }
        }
    }


}
