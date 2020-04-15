using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject indicator;
    bool onArea;
    public bool oneTime = true;

    public GameObject[] objectsToActivate;

    void Start()
    {
        
    }
    void Update()
    {
        if(onArea && Input.GetKeyDown(KeyCode.E))
        {
            Execute();
            indicator.SetActive(false);
            if (oneTime) Destroy(gameObject);
        }
    }

    public void Execute()
    {
        for(int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            onArea = true;
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            indicator.SetActive(false);
            onArea = false;
        }
    }
}
