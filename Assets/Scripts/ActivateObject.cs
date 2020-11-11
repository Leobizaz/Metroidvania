﻿using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public GameObject obj;
    public bool oneTime = true;
    bool once;
    public bool loadarea;
    public bool deActivate;

    public bool onInside = false;
    public bool onEnable = false;
    public float onEnableDelay = 0;

    private void OnEnable()
    {
        if (onEnable)
        {
            Invoke("Activate", onEnableDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onEnable) return;

        if (loadarea)
        {
            if (collision.tag == "Player")
            {
                Activate();
            }
            return;
        }

        if (collision.tag == "Player" && !once)
        {
            Activate();
            if (oneTime)
            {
                once = true;
                Destroy(gameObject, 0.2f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onInside)
        {
            if(collision.gameObject.tag == "Player")
            {
                obj.SetActive(false);
            }
        }
    }

    public void Activate()
    {
        if (deActivate)
        {
            obj.SetActive(false);
            return;
        }
        obj.SetActive(true);
    }
}
