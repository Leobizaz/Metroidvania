using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunshot : MonoBehaviour
{
    public float speed = 10f;
    bool moving;
    CircleCollider2D collider;

    private void Start()
    {
        moving = true;
        Destroy(gameObject, 2f);
        collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(moving)
            transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        collider.enabled = false;
        moving = false;
    }
}
