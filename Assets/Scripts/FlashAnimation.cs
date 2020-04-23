using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAnimation : MonoBehaviour
{
    PlayerController player;
    public GameObject flashFX;
    public GameObject spriteLanterna;
    public GameObject luzQacordaosBixo;
    AudioSource audioS;
    public AudioClip SFX_arranca;

    private void Awake()
    {
        player = transform.parent.GetComponent<PlayerController>();
        audioS = player.GetComponent<AudioSource>();
    }

    public void Play()
    {
        transform.parent.transform.position = new Vector3(0.29f, -90.655f, 0);
        transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.Freio();

        if (PlayerController.facingleft)
            player.FaceRight();

        player.isBusy = true;
        GetComponent<Animator>().Play("FlashCutscene");
    }

    public void RemoveSprite()
    {
        spriteLanterna.SetActive(false);
        audioS.PlayOneShot(SFX_arranca);
    }

    public void FlashFX()
    {
        flashFX.SetActive(true);
    }

    public void FinishAnimation()
    {
        luzQacordaosBixo.SetActive(true);
        player.isBusy = false;
        player.UnlockFlash();
    }
}
