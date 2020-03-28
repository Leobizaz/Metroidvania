using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject objectToFollow;

    private void Update()
    {
        this.transform.position = objectToFollow.transform.position;
    }
}
