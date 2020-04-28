using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealSecret : MonoBehaviour
{
    public bool onEnable;
    public bool hide;
    public HideSecret[] secrets;
    public bool oneTime = true;
    public float cooldownTime = 3;
    bool once;
    float cooldown;

    private void OnEnable()
    {
        if (onEnable)
        {
            Activate();
        }
    }

    private void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!once && cooldown <= 0)
            {
                if(oneTime)
                    once = true;

                Activate();
            }
        }
    }

    void Activate()
    {
        cooldown = cooldownTime;
        for(int i = 0; i < secrets.Length; i++)
        {
            if (hide) secrets[i].Hide();
            else secrets[i].Unhide();
        }
    }

}
