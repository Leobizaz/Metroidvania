using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPlatform : MonoBehaviour
{
    bool playerOnTop;
    private PlatformEffector2D effector;
    public float wait;
    float normalOffset;


    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        normalOffset = effector.rotationalOffset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerOnTop = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerOnTop = false;
        }
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") >= 0)
        {
            StartCoroutine(Wait());
        }

        if (Input.GetAxis("Vertical") <= -0.5f)
        {
            wait = 0;
            if (wait <= 0.5 && Input.GetKey(KeyCode.Space) && playerOnTop)
            {
                effector.rotationalOffset = normalOffset - 180; ;
              
            }
        }
        if (wait == 0.5f)
        {
            StopAllCoroutines();
            effector.rotationalOffset = normalOffset;
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        wait = 0.5f;
    }
}
