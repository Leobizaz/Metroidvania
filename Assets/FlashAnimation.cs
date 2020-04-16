using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAnimation : MonoBehaviour
{
    public void Play()
    {
        transform.parent.transform.position = new Vector3(0.29f, -90.655f, 0);
        transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.parent.transform.localScale = new Vector3(-1f, 1, 1);
    }
}
