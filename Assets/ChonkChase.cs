using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChonkChase : MonoBehaviour
{
    public Animator anim;
    public GameObject puppet;
    public GameObject cameraFocus;
    public GameObject player;
    bool chaseStarted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Light")
        {
            StartChase();
        }
    }

    private void Update()
    {
        if (chaseStarted)
        {
            if(Vector2.Distance(puppet.transform.position, player.transform.position) < 15)
            {
                cameraFocus.SetActive(true);
                anim.speed = 0.650f;
            }
            else if(Vector2.Distance(puppet.transform.position, player.transform.position) > 17)
            {
                anim.speed = 3.5f;
                cameraFocus.SetActive(false);
            }
            else
            {
                cameraFocus.SetActive(true);
                anim.speed = 1;
            }
        }
    }

    public void StartChase()
    {
        chaseStarted = true;
        anim.Play("Chase");
        cameraFocus.SetActive(true);
    }

}
