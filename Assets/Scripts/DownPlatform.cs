using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPlatform : MonoBehaviour
{

    private PlatformEffector2D effector;
    public float wait;


    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            StartCoroutine(Wait());
        }

        if (Input.GetKey(KeyCode.S) )
        {
            wait = 0;
            if (wait <= 0.5 && Input.GetKey(KeyCode.Space))
            {
                effector.rotationalOffset = 180f;
              
            }
        }
        if (wait == 0.5f)
        {
            StopAllCoroutines();
            effector.rotationalOffset = 0f;
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        wait = 0.5f;
    }
}
