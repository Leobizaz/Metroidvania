using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTags : MonoBehaviour
{
    public bool Efeito;
    public bool Ambiente;
    public bool Musica;

    public AudioSource audiosource;

    float volumeEfeitos;
    float volumeAmbiente;
    float volumeMusica;

    private void Awake()
    {
        audiosource = gameObject.GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        GetSavedVolumeOptions();

        if (Efeito) audiosource.volume = volumeEfeitos;
        if (Ambiente) audiosource.volume = volumeAmbiente;
        if (Musica) audiosource.volume = volumeMusica;

    }

    void GetSavedVolumeOptions()
    {
        volumeEfeitos = MenuController.Saved_volumeEfeitos;
        volumeAmbiente = MenuController.Saved_volumeAmbiente;
        volumeMusica = MenuController .Saved_volumeMusica;
    }
}
