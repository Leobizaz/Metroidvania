using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableDisableCollider : MonoBehaviour
{
    public PolygonCollider2D col;
    public float time;

    private void OnEnable()
    {
        col.enabled = true;
        Invoke("DisableCol", time);
    }

    void DisableCol()
    {
        col.enabled = false;
    }

}
