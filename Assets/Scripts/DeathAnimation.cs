using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

[ExecuteInEditMode]
public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    public Material normalMat;
    public Material silhouetteMat;
    public GameObject hud;
    //public Camera mainCamera;
    public LayerMask oldmask;
    public bool silhouette;
    public ParticleSystem[] airparticles;
    bool once1;
    bool once2;

    private void Awake()
    {
        Camera.main.GetComponent<CinemachineBrain>().enabled = true;
    }

    private void Update()
    {
        if (silhouette)
        {
            ChangeToSilhouette();
            return;
        }
        else if(!silhouette)
        {
            ChangeToNormal();
            return;
        }


    }

    public void DeathAnimationPlay()
    {
        for(int a = 0; a < airparticles.Length; a++)
        {
            airparticles[a].Play();
        }
    }

    public void ChangeToNormal()
    {
        if (!once2)
        {
            once2 = true;
            once1 = false;
            hud.SetActive(true);
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.material = normalMat;
                Camera.main.cullingMask = oldmask;
            }
        }
    }

    public void ChangeToSilhouette()
    {
        if (!once1)
        {
            once1 = true;
            once2 = false;
            hud.SetActive(false);
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.material = silhouetteMat;
                Camera.main.cullingMask = (1 << LayerMask.NameToLayer("DeathSprite"));
                Camera.main.GetComponent<CinemachineBrain>().enabled = false;
                Camera.main.GetComponent<Follow>().enabled = true;
            }
        }
    }

    public void DeathScreen()
    {
        Invoke("TryAgain", 5);
    }

    public void TryAgain()
    {
        PlayerController.demoJ1 = true;
        PlayerController.jogoNovo = false;
        SceneManager.LoadScene("CENAPRINCIPAL");
    }

}
