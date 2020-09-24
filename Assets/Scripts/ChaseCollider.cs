using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCollider : MonoBehaviour
{
    public GameObject fadeScreen;
    bool touched;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && !touched)
        {
            CancelInvoke("ResetChase");
            touched = true;
            fadeScreen.SetActive(true);
            fadeScreen.GetComponent<Animator>().Play("Fade", -1, 0);
            Invoke("ResetChase", 1f);
        }
    }

    public void ResetChase()
    {
        CancelInvoke("ResetTouched");
        GameEvents.current.ResetChase();
        Invoke("ResetTouched", 0.3f);
    }

    public void ResetTouched()
    {
        touched = false;
    }
}
