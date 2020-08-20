using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableJo : MonoBehaviour
{
    public static bool TriggerJoshOn;

    public GameObject IntJosh;
    private void Awake()
    {
        if (TriggerJoshOn == true)
        {
            Destroy(IntJosh);
        }
    }

    private void OnEnable()
    {
        TriggerJoshOn = true;
    }
}
