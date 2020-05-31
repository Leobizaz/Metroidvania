using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beacon : MonoBehaviour
{
    public string nome;
    public Vector3 posição;
    public GameObject canvas;
    public GameObject highLight;
    public GameObject luz;
    public Text nomeUI;
    public bool storyBeacon;
    public ParticleSystem FX;
    bool playerIn;
    bool once;
    bool online;

    private void Start()
    {
        posição = transform.position;
        luz.SetActive(false);
        Invoke("BeaconOnline", 0.5f);
    }

    private void Update()
    {
        if(playerIn && Input.GetKeyDown(KeyCode.E) && !once) //remover o beacon
        {
            BeaconOffline();
        }
    }

    void BeaconOnline() //manda um sinal para criar um novo beacon na lista
    {
        gameObject.name = nome;

        if (!storyBeacon)
            luz.SetActive(true);

        GameEvents.current.NewBeacon(this.gameObject);
        nomeUI.text = nome;
        online = true;
    }

    void BeaconOffline() //f beacon
    {
        once = true;
        online = false;
        GameEvents.current.RemoveBeacon(this.gameObject); //manda um sinal para remover da lista
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!storyBeacon)
        {
            if (collision.tag == "Player" && online)
            {
                FX.Play();
                playerIn = true;
                highLight.SetActive(true);
                canvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!storyBeacon)
        {
            if (collision.tag == "Player")
            {
                FX.Stop();
                playerIn = false;
                highLight.SetActive(false);
                canvas.SetActive(false);
            }
        }
    }




}
