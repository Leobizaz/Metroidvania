using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("KillEx", 2f);
    }

    void KillEx()
    {
        Destroy(this.gameObject);
    }
}
