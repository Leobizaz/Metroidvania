using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteFlashCutscene : MonoBehaviour
{
    public FlashAnimation flashAnim;
    public GameObject newEntry;
    private void OnEnable()
    {
        flashAnim.Play();
        LogUnlocker.current.UnlockLog(0);
    }
}
