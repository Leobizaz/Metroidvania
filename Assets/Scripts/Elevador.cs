﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Elevador : MonoBehaviour
{
    public int currentAndar = 3;
    bool first;
    public bool powerOn;
    Animator anim;
    AudioSource aSource;
    public AudioClip stompSFX;
    bool playerOnTop;
    bool moving;
    public Transform primeiroAndar;
    public Transform segundoAndar;
    public Transform terceiroAndar;
    public ParticleSystem stompFX;
    float distance;
    public GameObject jaula;
    public GameObject setaCima;
    public GameObject setaBaixo;

    public bool doisAndar;

    private void Start()
    {
        
        aSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        GameEvents.current.onReatorPowerUP += PowerUP;
        setaCima.SetActive(false);
        setaBaixo.SetActive(false);
    }

    private void Update()
    {
        if (powerOn || GameLoad.playerHasDiedOnce)
        {
            if (playerOnTop)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (currentAndar == 1)
                    {
                        return;
                    }
                    else
                    {
                        setaCima.SetActive(false);
                        setaBaixo.SetActive(true);
                        DOTween.Kill(this.transform);
                        CancelInvoke("Unlock");
                        moving = true;
                        jaula.SetActive(true);
                        aSource.Play();
                        currentAndar--;
                        if (doisAndar) currentAndar--;
                        if (currentAndar == 2 && !doisAndar)
                        {
                            distance = Vector3.Distance(this.transform.position, segundoAndar.position);
                            transform.DOMove(segundoAndar.position, distance/3).SetEase(Ease.Linear);
                        }
                        if (currentAndar == 1)
                        {
                            distance = Vector3.Distance(this.transform.position, primeiroAndar.position);
                            transform.DOMove(primeiroAndar.position, distance/3).SetEase(Ease.Linear);
                        }
                        Invoke("Unlock", distance/3);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    if (currentAndar == 3)
                    {
                        return;
                    }
                    else
                    {
                        setaBaixo.SetActive(false);
                        setaCima.SetActive(true);
                        DOTween.Kill(this.transform);
                        CancelInvoke("Unlock");
                        jaula.SetActive(true);
                        moving = true;
                        aSource.Play();
                        currentAndar++;
                        if (doisAndar) currentAndar++;
                        if (currentAndar == 2 && !doisAndar)
                        {
                            distance = Vector3.Distance(this.transform.position, segundoAndar.position);
                            transform.DOMove(segundoAndar.position, distance/3).SetEase(Ease.Linear);
                        }
                        if (currentAndar == 3)
                        {
                            distance = Vector3.Distance(this.transform.position, terceiroAndar.position);
                            transform.DOMove(terceiroAndar.position, distance/3).SetEase(Ease.Linear);
                        }
                        Invoke("Unlock", distance/3);
                    }
                }


            }
        }
    }

    void Unlock()
    {
        setaBaixo.SetActive(false);
        setaCima.SetActive(false);
        jaula.SetActive(false);
        aSource.Stop();
        moving = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!first && !powerOn && !GameLoad.playerHasDiedOnce)
            {
                GameEvents.current.ShakeCamera(0.5f, 0.6f, 1);
                stompFX.Play();
                first = true;
                anim.Play("elevador_stomp");
                return;
            }

            playerOnTop = true;
            collision.gameObject.transform.parent = this.gameObject.transform;
        }
    }

    public void ElevadorStompFX()
    {
        stompFX.Play();
        aSource.PlayOneShot(stompSFX);
    }

    public void PowerUP()
    {
        powerOn = true;
        anim.Play("elevador_fixed");
        first = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
            playerOnTop = false;
        }
    }
}
