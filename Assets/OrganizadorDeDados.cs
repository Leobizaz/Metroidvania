using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OrganizadorDeDados : MonoBehaviour
{


    public static OrganizadorDeDados current;
    public GameObject firstObject;
    public GameObject separador_tripulantes;
    public List<Button> buttons;
    public List<GameObject> panels;
    float slot;
    int tripulantes;
    int logNave;
    Vector3 initialPos;

    private void Start()
    {
        current = this;
    }

    private void OnEnable()
    {
        logNave = 0;
        tripulantes = 0;
        initialPos = firstObject.GetComponent<RectTransform>().localPosition;
        slot = initialPos.y - 100;
        foreach (Button button in buttons)
        {
            Log buttonlog = button.GetComponent<Log>();


            if (!buttonlog.unlocked)
            {
                RectTransform rt = button.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(initialPos.x + 3000, 0, rt.localPosition.z);
            }

            if (buttonlog.unlocked && buttonlog.logtag == "LogNave")
            {
                logNave++;
                RectTransform rt = button.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(initialPos.x, slot, initialPos.z);
                slot -= 100;
            }
        }

        RectTransform rt_separador1 = separador_tripulantes.GetComponent<RectTransform>();
        rt_separador1.localPosition = new Vector3(initialPos.x, slot, initialPos.z);
        slot -= 100;

        foreach (Button button in buttons)
        {
            Log buttonlog = button.GetComponent<Log>();

            if (buttonlog.unlocked && buttonlog.logtag == "LogTripulantes")
            {
                tripulantes++;
                RectTransform rt = button.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(initialPos.x, slot, initialPos.z);
                slot -= 100;
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
