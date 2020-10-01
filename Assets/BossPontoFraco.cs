using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPontoFraco : MonoBehaviour
{
    bool once;
    public Boss_C2 boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerWeapon" && !once)
        {
            once = true;
            boss.GetHit();
            Invoke("Reset", 2);
        }
    }

    private void Reset()
    {
        once = false;
    }
}
