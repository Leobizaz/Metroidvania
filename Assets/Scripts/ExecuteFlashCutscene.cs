using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteFlashCutscene : MonoBehaviour
{
    public FlashAnimation flashAnim;
    public GameObject newEntry;
    public static bool FlashLog;
    private void OnEnable()
    {
        flashAnim.Play();
        LogUnlocker.current.UnlockLog(0);
        FlashLog = true;
    }
    private void Awake()
    {
        if(FlashLog == true)
            LogUnlocker.current.UnlockLog(0);
    }
}
