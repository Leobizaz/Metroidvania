using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFaca : MonoBehaviour
{
    public GameObject indicator;
    bool onArea;
    public bool oneTime = true;
    public bool destroy = true;
    bool once;
    public GrabKnifeAnimation animatione;
    public GameObject objectToActivate;

    void Start()
    {
        indicator.SetActive(false);
    }
    void Update()
    {
        if (onArea && Input.GetKeyDown(KeyCode.E) && !once)
        {
            if (oneTime) once = true;
            Execute();
            indicator.SetActive(false);
            if (destroy)
            {
                Destroy(gameObject, 0.1f);
                Destroy(indicator, 0.1f);
            }
        }
    }

    public void Execute()
    {
        animatione.Play();
        ActivateObj();
    }

    void ActivateObj()
    {
        objectToActivate.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            onArea = true;
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            indicator.SetActive(false);
            onArea = false;
        }
    }
}
