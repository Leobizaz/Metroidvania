using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{

    [SerializeField]

    public AudioSource Begin;
    public Animator anim;

    public Dropdown DropdownQuality;
    public Dropdown DropdownResolution;
    public Toggle FullScreen;
    public TextMeshProUGUI Inicio;

    public float volumeEfeitos;
    public float volumeAmbiente;
    public float volumeMusica;

    public Slider sliderEfeitos;
    public Slider sliderAmbiente;
    public Slider sliderMusica;

    public static float Saved_volumeEfeitos;
    public static float Saved_volumeAmbiente;
    public static float Saved_volumeMusica;

    void Awake()
    {
        DropdownQuality.value = QualitySettings.GetQualityLevel();
        DropdownResolution.onValueChanged.AddListener(delegate
        {
            DropDownValueChanged(DropdownResolution);
        });
        GetSavedOptions();
        if (Inicio != null)
        {
            if (GameLoad.playerHasDiedOnce)
                Inicio.text = "Reinício";
            else
                Inicio.text = "Jogar";
        }
    }

    void Update()
    {

        volumeEfeitos = sliderEfeitos.value;
        volumeAmbiente = sliderAmbiente.value;
        volumeMusica = sliderMusica.value;

        SetSavedOptions();

        if (DropdownQuality.value == 0)
        {
            SetVeryLow();
        }
        if (DropdownQuality.value == 1)
        {
            SetLow();
        }
        if (DropdownQuality.value == 2)
        {
            SetMedium();
        }
        if (DropdownQuality.value == 3)
        {
            SetHigh();
        }
        if (DropdownQuality.value == 4)
        {

            SetVeryHigh();
        }

        if (DropdownQuality.value == 5)
        {
            SetUltra();
        }


        if (FullScreen.isOn == true)
        {
            Screen.fullScreen = true;
        }
        if (FullScreen.isOn == false)
        {
            Screen.fullScreen = false;
        }
    }

    void DropDownValueChanged(Dropdown change)
    {
        if (DropdownResolution.value == 0)
        {
            Resolution1024();
        }
        if (DropdownResolution.value == 1)
        {
            SetLow();
        }
        if (DropdownResolution.value == 2)
        {
            Resolution1280();
        }
        if (DropdownResolution.value == 3)
        {
            Resolution1280x();
        }
        if (DropdownResolution.value == 4)
        {

            Resolution1600();
        }

        if (DropdownResolution.value == 5)
        {
            Resolution1920();
        }
    }

    public void SetVeryLow()
    {

        QualitySettings.SetQualityLevel(0);
    }
    public void SetLow()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void SetMedium()
    {
        QualitySettings.SetQualityLevel(2);
    }
    public void SetHigh()
    {
        QualitySettings.SetQualityLevel(3);
    }
    public void SetVeryHigh()
    {
        QualitySettings.SetQualityLevel(4);
    }

    public void SetUltra()
    {
        QualitySettings.SetQualityLevel(5);
    }


    public void Resolution1024()
    {
        Screen.SetResolution(1024, 768, Screen.fullScreen);
    }
    public void Resolution1280()
    {
        Screen.SetResolution(1280, 720, Screen.fullScreen);
    }
    public void Resolution1280x()
    {
        Screen.SetResolution(1280, 1024, Screen.fullScreen);
    }
    public void Resolution1600()
    {
        Screen.SetResolution(1600, 900, Screen.fullScreen);
    }
    public void Resolution1920()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }

    public void PlayAudio()
    {
        Begin.Play();
        anim.Play("Menu Out");
        Invoke("LoadGame", 5f);
    }
    public void LoadGame()
    {
        if (GameLoad.playerHasDiedOnce)
            SceneManager.LoadScene("CENAPRINCIPAL");
        else
            SceneManager.LoadScene("Intro");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void UnPause()
    {
        Time.timeScale = 1;
    }
    void GetSavedOptions()
    {


        sliderEfeitos.value = Saved_volumeEfeitos;
        sliderAmbiente.value = Saved_volumeAmbiente;
        sliderMusica.value = Saved_volumeMusica;

        volumeEfeitos = Saved_volumeEfeitos;
        volumeAmbiente = Saved_volumeAmbiente;
        volumeMusica = Saved_volumeMusica;

    }
    void SetSavedOptions()
    {
        if (volumeAmbiente == 0 || volumeEfeitos == 0 || volumeMusica == 0)
        {
            volumeEfeitos = 0.3f;
            volumeAmbiente = 0.12f;
            volumeMusica = 0.12f;
            sliderEfeitos.value = volumeEfeitos;
            sliderAmbiente.value = volumeAmbiente;
            sliderMusica.value = volumeMusica;
        }
        Saved_volumeEfeitos = volumeEfeitos;
        Saved_volumeAmbiente = volumeAmbiente;
        Saved_volumeMusica = volumeMusica;
     
    }
}
