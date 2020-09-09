using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{

    public GameObject[] Enemies;

    public Transform[] pos;

    private void Awake()
    {

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
             if(Enemies[0]!= null)
                Instantiate(Enemies[0], pos[0]);
            if (Enemies[1] != null)
                Instantiate(Enemies[1], pos[1]);
            if (Enemies[2] != null)
                Instantiate(Enemies[2], pos[2]);
            if (Enemies[3] != null)
                Instantiate(Enemies[3], pos[3]);
            if (Enemies[4] != null)
                Instantiate(Enemies[4], pos[4]);
        }
    }
}
