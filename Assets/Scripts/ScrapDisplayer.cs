using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScrapDisplayer : MonoBehaviour
{
    public static ScrapDisplayer current;
    public Animator popup;
    public Text display;
    float value;
    float currentvalue;
    bool updating;

    private void Start()
    {
        current = this;
        GameEvents.current.onUpdateScrap += OnUpdateScrap;
    }

    private void Update()
    {
        if (currentvalue < value)
        {
            int ScoreIncrement = Mathf.RoundToInt(Time.unscaledDeltaTime * 60);
            currentvalue += ScoreIncrement;
            if(currentvalue > value)
                currentvalue = value;

            display.text = currentvalue.ToString();
        }
        else if (currentvalue > value)
        {
            int ScoreDecrement = Mathf.RoundToInt(Time.unscaledDeltaTime * 60);
            currentvalue -= ScoreDecrement;
            if (currentvalue < value)
                currentvalue = value;

            display.text = currentvalue.ToString();
        }

    }

    public void DisplayScrap()
    {
        if (!updating)
        {
            popup.Play("scrapPopup");
            updating = true;
            Invoke("DisableUpdating", 3);
        }
    }

    void OnUpdateScrap(float ammount, bool subtract)
    {
        updating = true;
        currentvalue = float.Parse(display.text);
        value = ammount;
        popup.Play("scrapPopup");
        if(!subtract)
        Invoke("UpdateValue", 3);
        else
        {
            Invoke("SubtractValue", 3);
        }

    }



    void DisableUpdating()
    {
        updating = false;
    }

    void SubtractValue()
    {
        updating = false;
        GameController.currentScrap = value;
        Debug.Log(GameController.currentScrap);
    }

    void UpdateValue()
    {
        updating = false;
        GameController.currentScrap = value;
        Debug.Log(GameController.currentScrap);
    }

}
