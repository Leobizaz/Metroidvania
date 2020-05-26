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
    public GameObject Inventario;
    public GameObject Opcoes;
    public GameObject Data;
    private bool pause = false;
    bool temNotificação;
    public GameObject notificação;

    private void Start()
    {
        GameEvents.current.onNewLogUnlocked += SetNotification; 
    }

    void SetNotification()
    {
        temNotificação = true;
    }



    // Update is called once per frame
    void Update()
    {
        if (PausePannel != null)
        {
            if (PlayerController.hits < 2)
            {
                health1.SetActive(false);
            }
            if (PlayerController.hits <= 1.5)
            {
                health2.SetActive(false);
            }
            if (PlayerController.hits <= 0)
                health3.SetActive(false);
        }
        if(Input.GetKeyDown (KeyCode.P) && pause == false && !PromptNome.current.nomeando)
        {
            if (PausePannel != null)
            {
                PausePannel.SetActive(true);
                Time.timeScale = 0;
                pause = true;
                if (temNotificação)
                {
                    temNotificação = false;
                    notificação.SetActive(true);
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausePannel != null && pause == true)
            {
                Inventario.SetActive(true);
                Data.SetActive(false);
                Opcoes.SetActive(false);
                PausePannel.SetActive(false);
                Time.timeScale = 1;
                pause = false;
            }
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
