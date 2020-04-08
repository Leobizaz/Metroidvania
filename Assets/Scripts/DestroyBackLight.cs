using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBackLight : MonoBehaviour
{
    public GameObject BackLight;
    public static bool backLight = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            BackLight.SetActive(false);
            backLight = false;
            ////////////Tocar Som//////////////
        }
    }
}
