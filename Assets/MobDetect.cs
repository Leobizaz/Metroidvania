using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDetect : MonoBehaviour
{
    public bool detected;
    public bool destroy;
    public bool playSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Light" && !detected)
        {
            detected = true;
            if(playSound)
                GameObject.Find("Audio").GetComponent<UniversalSoundsManager>().PlaySpookSound();
            if (destroy) Destroy(gameObject, 4f);
        }
    }


}
