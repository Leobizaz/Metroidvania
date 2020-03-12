using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTrigger : MonoBehaviour
{
    public GameObject exit;
    public RopeController rope;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            rope.ClimbOff();
            collision.gameObject.transform.position = exit.transform.position;
        }
    }
}
