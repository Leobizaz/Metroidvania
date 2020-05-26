using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraDeCura : MonoBehaviour
{
    public PlayerController player;
    public GameObject indicator;
    bool onArea;
    bool once;
    private float timer;
    private bool timeOn;

    private void Start()
    {
        timeOn = false;
        timer = 0;
        indicator.SetActive(false);
    }

    void Update()
    {
        if (onArea && Input.GetKeyDown(KeyCode.E))
        {
            indicator.SetActive(false);
            timeOn = true;

        }
        if(timeOn)
            timer += Time.deltaTime;

        if (timer >= 5f)
        {
            Execute();
        }
    }

    public void Execute()
    {
        PlayerController.hits = 3;
        timer = 0f;
        timeOn = false;
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
