using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvoDoBoss : MonoBehaviour
{
    public GameObject particleFX;
    public Boss_C2 bossScript;
    bool once;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            if (!once)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        once = true;
        particleFX.SetActive(true);
        particleFX.transform.parent = null;
        particleFX.transform.position = new Vector3(particleFX.transform.position.x, particleFX.transform.position.y, 0);
        bossScript.GetHitBig();
        Destroy(gameObject);

    }

}
