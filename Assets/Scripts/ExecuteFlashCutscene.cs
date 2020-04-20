using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteFlashCutscene : MonoBehaviour
{
    public FlashAnimation flashAnim;
    private void Start()
    {
        flashAnim.Play();
    }
}
