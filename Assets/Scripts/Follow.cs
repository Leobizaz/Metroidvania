using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject objectToFollow;
    public bool ignoreZ;

    private void Update()
    {
        if(!ignoreZ)
        this.transform.position = objectToFollow.transform.position;
        else
        {
            this.transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, transform.position.z);
        }
    }
}
