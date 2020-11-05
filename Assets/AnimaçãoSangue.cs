using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaçãoSangue : MonoBehaviour
{
    PlayerController anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim = collision.gameObject.GetComponent<PlayerController>();
            anim.isBusy = true;
            Invoke("DoIt", 15.5f);
        }
    }

    void DoIt()
    {
        anim.AnimationSangue();
        gameObject.SetActive(false);
    }
}
