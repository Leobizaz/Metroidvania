using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunshot : MonoBehaviour
{
    public float speed = 10f;
    bool moving;
    CircleCollider2D c_collider;
    
    public GameObject gunExplosion;
    public GameObject Reflect;
    Quaternion rot180degrees;
    Vector3 Bruh;

    private void Start()
    {
        GameEvents.current.MakeBigSound(gameObject);
        moving = true;
        Destroy(gameObject, 2f);
        c_collider = GetComponent<CircleCollider2D>();
        Bruh = new Vector3(0, 0, 180f);
    }

    private void Update()
    {
        if(moving)
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        Quaternion AAAAAA = Quaternion.Euler(Bruh);
        rot180degrees = transform.rotation * AAAAAA;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Light" && other.gameObject.tag != "Damage" && other.gameObject.tag != "Trigger" && other.gameObject.tag != "Reflect")
        {
            Debug.Log("Gun hit object " + other.name);
            c_collider.enabled = false;
            moving = false;
            //     gunExplosion.transform.position = this.gameObject.transform.position;
            Instantiate(gunExplosion, transform.position, transform.rotation);
            GameEvents.current.MakeBigSound(gameObject);
         //   Destroy(gameObject);
        }
        if(other.gameObject.tag == "Reflect")
        {
            Debug.Log("Gun hit object " + other.name);
            c_collider.enabled = false;
            moving = false;
            Instantiate(Reflect, transform.position, rot180degrees);
        }
    }

}
