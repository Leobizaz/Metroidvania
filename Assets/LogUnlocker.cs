using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogUnlocker : MonoBehaviour
{
    public static LogUnlocker current;
    public Log[] logs;

    private void Awake()
    {
        current = this;
    }

    public void UnlockLog(int id)
    {
        logs[id].unlocked = true;
    }

}
