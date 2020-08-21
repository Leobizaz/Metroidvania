using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateParticle : MonoBehaviour
{
    GameObject parent;
    public float rotationLeft;
    public float rotationRight;


    private void Start()
    {
        parent = transform.parent.gameObject;

    }

    private void Update()
    {
        if(this.transform.position.x < parent.transform.position.x)
        {
            //is on left
            transform.rotation = Quaternion.Euler(0,0,rotationLeft);
        }
        else
        {
            // is on right
            transform.rotation = Quaternion.Euler(0, 0, rotationRight);

        }
    }

}
