using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public int EnemyCount;
    public string EnemyType;
    public Transform[] pos;

    public GameObject Fotoverme;
    public GameObject Outro;

    private void Awake()
    {
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
           // if (EnemyType == "Fotoverme" && EnemyCount == 1)
            //{
                Instantiate(Fotoverme, pos[0]);
                
           // }
            if (EnemyType == "Fotoverme" && EnemyCount == 2)
            {
                Instantiate(Fotoverme, pos[1]);
              
            }
            if (  EnemyType == "Fotoverme" && EnemyCount == 3)
            {
                Instantiate(Fotoverme, pos[2]);
              
            }
            if ( EnemyType == "Fotoverme" && EnemyCount == 4)
            {
                Instantiate(Fotoverme, pos[3]);
               
            }
            if (EnemyType == "Fotoverme" && EnemyCount == 5)
            {
                Instantiate(Fotoverme, pos[4]);
              
            }
        }
    }
}
