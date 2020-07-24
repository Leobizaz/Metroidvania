using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScrapDisplayer : MonoBehaviour
{
    public Animator popup;
    public Text display;
    float value;
    float currentvalue;

    private void Start()
    {
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
    }

    void OnUpdateScrap(float ammount)
    {
        currentvalue = float.Parse(display.text);
        value = ammount;
        popup.Play("scrapPopup");
        Invoke("UpdateValue", 3);

    }

    void UpdateValue()
    {
        GameController.currentScrap += value;
    }

}
