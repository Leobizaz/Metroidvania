using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraDeCura : MonoBehaviour
{
    public PlayerController player;
    public GameObject indicator;
    bool onArea;
    bool once;

    private void Start()
    {
        indicator.SetActive(false);
    }

    void Update()
    {
        if (onArea && Input.GetKeyDown(KeyCode.E))
        {
            Execute();
            indicator.SetActive(false);
        }
    }

    public void Execute()
    {
        PlayerController.hits = 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PlayerController.hits < 3)
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
