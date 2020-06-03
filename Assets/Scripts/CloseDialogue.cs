using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDialogue : MonoBehaviour
{
    public GameObject dialogo;
    private void OnEnable()
    {
        DialoguePlayer player = dialogo.GetComponentInChildren<DialoguePlayer>();
        player.skipped = true;
        if (player.isPlaying)
        {
            player.Close();
        }
    }
}
