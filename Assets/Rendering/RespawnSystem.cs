using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public GameObject[] Enemies;
    public Transform[] pos;
    public int EnemyCount;
    public bool[] spawned;

    private void Awake()
    {
        DesativaSpawn();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (EnemyCount >= 1 && spawned[0] == false)
            {
                Instantiate(Enemies[0], pos[0].position, pos[0].rotation);
                Debug.Log("Surgiu 1 puto");
                Invoke("DesativaSpawn", 50f);
                spawned[0] = true;
            }
            if (EnemyCount >= 2 && spawned[1] == false)
            {
                Instantiate(Enemies[1], pos[1].position, pos[1].rotation);
                Debug.Log("Surgiu 2 puto");
                Invoke("DesativaSpawn", 50f);
                spawned[1] = true;
            }
            if (EnemyCount >= 3 && spawned[2] == false)
            {
                Instantiate(Enemies[2], pos[2].position, pos[2].rotation);
                Debug.Log("Surgiu 3 puto");
                Invoke("DesativaSpawn", 50f);
                spawned[2] = true;
            }
            if (EnemyCount >= 4 && spawned[3] == false)
            {
                Instantiate(Enemies[3], pos[3].position, pos[3].rotation);
                Debug.Log("Surgiu 4 puto");
                Invoke("DesativaSpawn", 50f);
                spawned[3] = true;
            }
            if (EnemyCount >= 5 && spawned[4] == false)
            {
                Instantiate(Enemies[4], pos[4].position, pos[4].rotation);
                Debug.Log("Surgiu 5 puto");
                Invoke("DesativaSpawn", 50f);
                spawned[4] = true;
            }
        }
    }
    void DesativaSpawn()
    {
        spawned[0] = false;
        spawned[1] = false;
        spawned[2] = false;
        spawned[3] = false;
        spawned[4] = false;
    }
}
