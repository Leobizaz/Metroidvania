using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarBeacon : MonoBehaviour
{
    public GameObject indicator;
    bool onArea;
    bool once;

    public GameObject popup;
    public PlayerController playerControl;

    void Start()
    {
        indicator.SetActive(false);
    }
    void Update()
    {
        if (onArea && Input.GetKeyDown(KeyCode.E) && !once)
        {
            once = true;
            Execute();
            indicator.SetActive(false);
            Destroy(gameObject, 0.2f);
        }
        if (playerControl.unlockedBeacon == true)
            Destroy(this.gameObject);
    }

    public void Execute()
    {
        playerControl.unlockedBeacon = true;
        popup.SetActive(true);
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
