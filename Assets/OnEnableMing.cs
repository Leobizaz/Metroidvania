using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableMing : MonoBehaviour
{
    public static bool TriggerMingOn;
    public GameObject IntMing;
    private void Awake()
    {
        if(TriggerMingOn == true)
        {
            Destroy(IntMing);
        }
    }
    private void OnEnable()
    {
        TriggerMingOn = true;
    }
}


