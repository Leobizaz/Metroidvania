using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLog : MonoBehaviour
{
    public bool onEnable;
    public int id;
    public static bool[] Log;
    private void OnEnable()
    {
        if (onEnable)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        LogUnlocker.current.UnlockLog(id);
        Log[id] = true;
    }

}
