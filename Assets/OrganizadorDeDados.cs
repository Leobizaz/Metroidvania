using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OrganizadorDeDados : MonoBehaviour
{


    public static OrganizadorDeDados current;
    public List<Button> buttons;
    public List<GameObject> panels;
    float slot;
    Vector3 initialPos;

    private void Start()
    {
        current = this;
    }

    private void OnEnable()
    {
        initialPos = buttons[0].GetComponent<RectTransform>().localPosition;
        slot = initialPos.y;
        foreach (Button button in buttons)
        {
            if (button.GetComponent<Log>().unlocked)
            {
                RectTransform rt = button.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(initialPos.x, slot, initialPos.z);
                slot -= 100;
            }
            else
            {
                RectTransform rt = button.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(initialPos.x + 3000, 0, rt.localPosition.z);
            }
        }
    }

    public void ResetInteractables()
    {
        for(int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(false);
        }

        foreach(Button button in buttons)
        {
            button.interactable = true;
        }
    }

}
