using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PromptNome : MonoBehaviour
{
    public static PromptNome current;
    public string currentNome;
    public GameObject panel;
    public InputField inputField;
    public BeaconPlacer beaconPlacer;
    public GameObject text;
    public bool nomeando;

    void Start()
    {
        current = this;
        panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    public void StartNaming() //inicia a tela de nomeação do beacon
    {
        panel.SetActive(true); //abre a janela de nomeação
        nomeando = true;
        EventSystem.current.SetSelectedGameObject(text); //seleciona o input field pra n ter q clicar nele pra digitar
        inputField.ActivateInputField(); //msm coisa do de cima XD
        inputField.text = null;
        Time.timeScale = 0; //za warudo
        currentNome = null;
    }

    public void Cancel()
    {
        nomeando = false;
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void EndNaming() //assim que o player termina de digitar no InputField, essa função é acionada pelo componente (tipo aqueles event de botão)
    {
        if (inputField.text == null) //cancela a colocação do beacon se o nome não for preenchido
        {
            Cancel(); 
            return;
        }
        nomeando = false;
        Time.timeScale = 1;
        currentNome = inputField.text;
        beaconPlacer.PlaceBeacon(currentNome); //finalmente coloca o beacon (e passa o nome que foi digitado pra ele)
        panel.SetActive(false); //fecha a janela de nomeação
    }



}
