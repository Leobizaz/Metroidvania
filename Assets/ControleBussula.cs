using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleBussula : MonoBehaviour
{
    public BeaconList beaconList;
    public GUIPointer ponteiro;
    public Text nameIndicator;
    bool safe;
    [SerializeField] int index = 0;

    private void Start()
    {
        ponteiro.gameObject.SetActive(false);
        nameIndicator.gameObject.SetActive(false);
        GameEvents.current.onNewBeacon += Startup;
        GameEvents.current.onRemoveBeacon += Close;
    }

    void Close(GameObject obj) // desliga o ponteiro se não houver nenhum sinalizador
    {
        if (beaconList.beacons.Count == 1) 
        {
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
            if (Input.GetKeyDown(KeyCode.DownArrow)) //setas alternam entre a lista de sinalizadores
            {
                index--;
                if (index < 0) index = beaconList.beacons.Count - 1;
                ponteiro.target = beaconList.beacons[index].transform;
                nameIndicator.text = beaconList.beacons[index].name;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
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
    }

}
