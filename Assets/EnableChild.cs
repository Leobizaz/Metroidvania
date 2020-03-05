using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableChild : MonoBehaviour
{
    public GameObject child;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            child.SetActive(true);
        }
    }
}
