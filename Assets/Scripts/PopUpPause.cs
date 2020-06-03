using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPause : MonoBehaviour
{
    public Animator anim;

    private void OnEnable()
    {
        anim.Play("Open");
        GameEvents.current.PausePlayer();
        PromptNome.current.nomeando = true;


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            Close();
        }
    }

    public void Close()
    {
        PromptNome.current.nomeando = false;
        GameEvents.current.UnPausePlayer();
        anim.Play("Close");
    }
}
