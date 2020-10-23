using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    GameObject player;
    bool facing = true;
    public float speed = 5;
    public ParticleSystem spawnEmitter;

    private void Start()
    {
        player = GameObject.Find("Player");
        Invoke("StopFacing", 3);
    }

    private void Update()
    {
        if (facing)
        {
            FacePlayer();
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Walkable")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().GetHitSpecific(null);
            Destroy(gameObject);
        }
    }

    void StopFacing()
    {
        facing = false;
        spawnEmitter.Stop();
        Invoke("Despawn", 6);
    }

    void FacePlayer()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000);
    }

    void Despawn()
    {
        Destroy(gameObject);
    }

}
