using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuebraParede : MonoBehaviour
{
    public bool isTrigger = true;
    public FakeWall parede;
    bool once;

    private void Start()
    {
        GameEvents.current.onChaseReset += OnReset;
    }

    void OnReset()
    {
        once = false;
    }

    private void OnEnable()
    {
        once = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrigger)
        {
            if (collision.gameObject.tag == "Player" && !once)
            {
                once = true;
                Quebra();
            }
        }
    }

    public void Quebra()
    {
        parede.ForceBreak();

        parede.sprite.gameObject.SetActive(false);
        GameEvents.current.ShakeCamera(1, 1, 0.5f);
    }
}
