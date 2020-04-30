using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spores : MonoBehaviour
{
    public GameObject SporesFx;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Instantiate(SporesFx, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
