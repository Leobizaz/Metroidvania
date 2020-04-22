using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public AudioSource Begin;
    public Animator anim;

    public void PlayAudio()
    {
        Begin.Play();
        anim.Play("Menu Out");
        Invoke("LoadGame", 5f);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("CENAPRINCIPAL");
    }
}
