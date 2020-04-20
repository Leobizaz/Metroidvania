using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public GameObject PausePannel;
    private bool pause = false;

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.hits <= 2)
        {
            health1.SetActive(false);
        }
        else if (PlayerController.hits <= 1)
        {
            health2.SetActive(false);
        }
        else if(PlayerController.hits <= 0)
            health3.SetActive(false);
        
        if(Input.GetKeyDown (KeyCode.P) && pause == false)
        {
            PausePannel.SetActive(true);
            Time.timeScale = 0;
            pause = true;
        }
        else if(Input.GetKeyDown(KeyCode.P) && pause == true)
        {
            PausePannel.SetActive(false);
            Time.timeScale = 1;
            pause = false;
        }
    }

    public void Menu()
    {
       // SceneManager.LoadScene();
    }
}
