using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleBussula : MonoBehaviour
{
    public BeaconList beaconList;
    public GUIPointer ponteiro;
    public Text nameIndicator;
    public Text countIndicator;
    public GameObject arrows;
    public Color colorSafe;
    public Color colorOof;
    bool safe;
    [SerializeField] int index = 0;

    private void Start()
    {
        arrows.SetActive(false);
        ponteiro.gameObject.SetActive(false);
        nameIndicator.gameObject.SetActive(false);
        GameEvents.current.onNewBeacon += Startup;
        GameEvents.current.onRemoveBeacon += Close;
    }

    void Close(GameObject obj) // desliga o ponteiro se não houver nenhum sinalizador
    {
        countIndicator.text = (4 - beaconList.beacons.Count).ToString();
        if (countIndicator.text != "0") countIndicator.color = colorSafe;
        if (countIndicator.text == "0") countIndicator.color = colorOof;

        if (beaconList.beacons.Count == 1)
        {
            arrows.SetActive(false);
            ponteiro.target = beaconList.beacons[0].gameObject.transform;
            nameIndicator.text = beaconList.beacons[0].gameObject.name;

        }

        if (beaconList.beacons.Count == 0) 
        {
            arrows.SetActive(false);
            ponteiro.target = null;
            ponteiro.gameObject.SetActive(false);
            nameIndicator.text = null;
            nameIndicator.gameObject.SetActive(false);
            safe = false;
        }
    }

    private void Update()
    {
        if (safe) // "safe" indica que tem pelo menos um sinalizador presente
        {
            if (Input.GetKeyDown(KeyCode.N)) //setas alternam entre a lista de sinalizadores
            {
                index--;
                if (index < 0) index = beaconList.beacons.Count - 1;
                ponteiro.target = beaconList.beacons[index].transform;
                nameIndicator.text = beaconList.beacons[index].name;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                index++;
                if (index > beaconList.beacons.Count - 1) index = 0;
                ponteiro.target = beaconList.beacons[index].transform;
                nameIndicator.text = beaconList.beacons[index].name;
            }


        }
    }

    void Startup(GameObject obj) //ocorre assim que o primeiro sinalizador é criado
    {
        countIndicator.text = (4 - beaconList.beacons.Count).ToString();
        if (countIndicator.text != "0") countIndicator.color = colorSafe;
        if (countIndicator.text == "0") countIndicator.color = colorOof;

        if (!safe)
        {
            //ativa o ponteiro e já seleciona o primeiro sinalizador
            index = 0;
            safe = true;
            ponteiro.gameObject.SetActive(true);
            ponteiro.target = obj.transform;
            nameIndicator.gameObject.SetActive(true);
            nameIndicator.text = obj.name;
        }
        if (safe)
        {
            if(beaconList.beacons.Count > 1)
            {
                arrows.SetActive(true);
            }
        }

    }

}
