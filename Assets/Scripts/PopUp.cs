using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    bool alive = false;
    public float duration;
    float t;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.Play("Open");
    }

    private void OnEnable()
    {
        alive = true;
    }

    private void Update()
    {
        if (alive)
        {
            t += Time.deltaTime;
            if(t >= duration)
            {
                Close();
                alive = false;
            }
        }
    }

    public void Close()
    {
        anim.Play("Close");
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
