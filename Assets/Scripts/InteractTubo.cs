using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTubo : MonoBehaviour
{
    public GameObject indicatorEntrar;
    public GameObject indicatorCortar;
    bool onArea;
    public bool oneTime = true;
    public bool destroy = true;
    bool once;
    bool open;
    public GameObject corteSprite;
    public ParticleSystem corteBurst;
    public GameObject cameraPLAYER;

    public GameObject camera;
    public GameObject player;
    public GameObject endposition;
    public GameObject triggerAnim;

    void Start()
    {
        indicatorEntrar.SetActive(false);
        indicatorCortar.SetActive(true);
    }
    void Update()
    {
        if (onArea && Input.GetKeyDown(KeyCode.E) && !once && open)
        {
            if (oneTime) once = true;
            Execute();
            indicatorEntrar.SetActive(false);
            if (destroy)
            {
                Destroy(gameObject, 0.1f);
                Destroy(indicatorEntrar, 0.1f);
            }
        }
    }

    public void Execute()
    {
        camera.SetActive(true);
        triggerAnim.SetActive(true);
        cameraPLAYER.SetActive(false);
        Invoke("Teleport", 2);
    }

    public void Teleport()
    {
        player.transform.position = endposition.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerWeapon")
        {
            if (collision.name == "fx_SlashSmall" || collision.name == "fx_SlashBig")
            {
                open = true;
                corteBurst.Play();
                corteSprite.SetActive(true);
                onArea = true;
                indicatorEntrar.SetActive(true);
                indicatorCortar.SetActive(false);
            }
        }


        if (collision.tag == "Player" && open)
        {
            onArea = true;
            indicatorEntrar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && open)
        {
            indicatorEntrar.SetActive(false);
            onArea = false;
        }
    }
}
