using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaCreepy : MonoBehaviour
{
    [SerializeField] bool detected;
    public GameObject target;
    GameObject player;
    public float speed = 1;
    public Vector3 idlePosition;

    private void Start()
    {
        idlePosition = target.transform.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            detected = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            detected = false;
        }
    }

    private void Update()
    {
        if (detected)
        {
            target.transform.position = Vector3.Lerp(target.transform.position, player.transform.position, Time.deltaTime * speed);
        }
        else
        {
            target.transform.localPosition = Vector3.Lerp(target.transform.localPosition, idlePosition, Time.deltaTime * speed);
        }
    }

}
