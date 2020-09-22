using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTubo : MonoBehaviour
{
    public GameObject indicator;
    bool onArea;
    public bool oneTime = true;
    public bool destroy = true;
    bool once;

    public GameObject cameraPLAYER;

    public GameObject camera;
    public GameObject player;
    public GameObject endposition;

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
        camera.SetActive(true);
        cameraPLAYER.SetActive(false);
        Invoke("Teleport", 2);
    }

    public void Teleport()
    {
        player.transform.position = endposition.transform.position;
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
