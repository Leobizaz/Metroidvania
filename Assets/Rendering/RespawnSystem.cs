using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public GameObject[] Enemies;
    public Transform[] pos;
    public int EnemyCount;

    private void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (EnemyCount >= 1 && EnemyCount < 2)
            {
                Instantiate(Enemies[0], pos[0]);
                this.gameObject.SetActive(true);
            }
            if (EnemyCount >= 2 && EnemyCount < 3)
            {
                Instantiate(Enemies[1], pos[1]);
                this.gameObject.SetActive(true);
              
            }
            if (EnemyCount >= 3 && EnemyCount < 4 )
            {
                Instantiate(Enemies[2], pos[2]);
                this.gameObject.SetActive(true);
            }
            if (EnemyCount >= 4 && EnemyCount < 5)
            {
                Instantiate(Enemies[3], pos[3]);
                this.gameObject.SetActive(true);
            }
            if (EnemyCount >= 5 && EnemyCount <= 1)
            {
                Instantiate(Enemies[4], pos[4]);
                this.gameObject.SetActive(true);
            }
        }
    }
}
