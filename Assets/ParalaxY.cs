using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxY : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        //make sure these are ."y"s    
        startpos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        float temp = (cam.transform.position.y * (1 - parallaxEffect));
        float dist = (cam.transform.position.y * parallaxEffect);

        //make sure the x poition stays the same, and the y is changine with "dist" 
        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);

    }
}
