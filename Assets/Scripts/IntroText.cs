using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class IntroText : MonoBehaviour
{
    public Text[] textos;
    public GameObject helper;
    public Animator menuAnim;
    public float textDuration = 3;
    int index = 0;
    public int finishingIndex;
    bool blocked;
    string currentText;
    float nextCooldown;

    private void Awake()
    {
        for(int a = 0; a < textos.Length; a++)
        {
            textos[a].DOFade(0, 0);
        }
        DeleteOld();
    }

    private void Update()
    {
        if (nextCooldown <= 0 && Input.GetKeyDown(KeyCode.E))
        {
            if(index == finishingIndex)
            {
                FinishAction();
                return;
            }
            if (blocked)
            {
                FinishText();
                nextCooldown = 1;
                return;
            }
            DeleteOld();
            nextCooldown = 1;
        }
        else
        {
            nextCooldown -= Time.deltaTime;
        }
    }

    void DeleteOld()
    {
        helper.SetActive(false);
        if(index > 0)
        {
            textos[index - 1].DOFade(0, 1);
            Invoke("PlayNext", 1);
            return;
        }
        PlayNext();
    }

    void PlayNext()
    {
        blocked = true;
        currentText = textos[index].text;
        textos[index].DOFade(1, textDuration).SetEase(Ease.Linear);
        Invoke("FinishText", textDuration);
    }

    void FinishText()
    {
        helper.SetActive(true);
        DOTween.Kill(textos[index].gameObject);
        CancelInvoke("FinishText");
        blocked = false;
        textos[index].text = currentText;
        if(index < textos.Length)
            index++;
    }

    void FinishAction()
    {
        helper.SetActive(false);
        textos[index].DOFade(1, textDuration).SetEase(Ease.Linear);
        Invoke("BlackScreen", 4);
    }

    void BlackScreen()
    {
        menuAnim.speed = 3;
        menuAnim.Play("Menu Out");
        Invoke("LoadScene", 5);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("CENAPRINCIPAL");
    }
}
