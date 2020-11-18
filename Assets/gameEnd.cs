using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameEnd : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("LoadMenu", 8);
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
