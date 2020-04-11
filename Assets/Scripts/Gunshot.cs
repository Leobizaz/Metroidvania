using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunshot : MonoBehaviour
{
    public float speed = 10f;
    bool moving;
    CircleCollider2D c_collider;
    
    public GameObject gunExplosion;

    private void Start()
    {
        moving = true;
        Destroy(gameObject, 2f);
        c_collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(moving)
            transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Light" && other.gameObject.tag != "Damage" && other.gameObject.tag != "Trigger")
        {
            Debug.Log("Gun hit object " + other.name);
            c_collider.enabled = false;
            moving = false;
       //     gunExplosion.transform.position = this.gameObject.transform.position;
            Instantiate(gunExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
