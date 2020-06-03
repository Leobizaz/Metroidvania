using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss_Cura : MonoBehaviour
{
    public GameObject indicator;
    public GameObject moss;
    bool onArea;
    public bool oneTime = true;
    bool once;

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
        }
    }

    public void Execute()
    {
        PlayerController.hits += 1;
        moss.SetActive(false);
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
