using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChonkChase : MonoBehaviour
{
    public Animator anim;
    public GameObject puppet;
    public GameObject cameraFocus;
    public GameObject player;
    bool chaseStarted;
    public bool ignoreSpeed;
    public float currentSpeed;
    public GameObject quebraParedeTrigger;
    bool touched;
    AudioSource audioS;

    private void Start()
    {
        GameEvents.current.onChaseReset += OnReset;
        audioS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Light" && !chaseStarted)
        {
            StartChase();
        }
    }

    private void Update()
    {
        if (chaseStarted)
        {
            currentSpeed = anim.speed;
            if (!ignoreSpeed)
            {
                if (Vector2.Distance(puppet.transform.position, player.transform.position) < 15)
                {
                    cameraFocus.SetActive(true);
                    //anim.speed = 0.350f;
                    anim.speed = Mathf.Lerp(anim.speed, 0.650f, Time.deltaTime * 4);
                }
                else if (Vector2.Distance(puppet.transform.position, player.transform.position) > 25)
                {
                    //anim.speed = 3.5f;
                    anim.speed = Mathf.Lerp(anim.speed, 3.5f, Time.deltaTime / 1.5f);
                    cameraFocus.SetActive(false);
                }
                else
                {
                    cameraFocus.SetActive(true);
                    //anim.speed = 1;
                    anim.speed = Mathf.Lerp(anim.speed, 1f, Time.deltaTime * 2);
                }
            }
        }
    }

    public void OnReset()
    {
        PlayerController playerCode = player.GetComponent<PlayerController>();
        player.transform.position = new Vector3(-17.72f, -135.83f, 0);

        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        playerCode.Freio();

        if (!PlayerController.facingleft)
            playerCode.FaceLeft();

        StartChase();
        touched = false;
    }

    public void ResetChase()
    {
        GameEvents.current.ResetChase();
    }

    public void StartChase()
    {
        audioS.Play();
        quebraParedeTrigger.SetActive(true);
        chaseStarted = true;
        anim.Play("Chase", -1, 0f);
        ignoreSpeed = false;
        anim.speed = 1;
        cameraFocus.SetActive(true);
    }

}
