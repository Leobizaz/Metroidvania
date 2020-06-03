using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockFeet : MonoBehaviour
{
    Vector3 localpos;

    private void Start()
    {
        localpos = new Vector3(0, -0.75f, 0);
    }

    private void Update()
    {
        transform.localPosition = localpos;
    }
}
