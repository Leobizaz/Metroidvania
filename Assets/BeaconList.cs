using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconList : MonoBehaviour
{
    public List<GameObject> beacons;

    void Start()
    {
        GameEvents.current.onNewBeacon += NewBeacon;
        GameEvents.current.onRemoveBeacon += RemoveBeacon;
    }

    void RemoveBeacon(GameObject obj)
    {
        beacons.Remove(obj);
    }

    void NewBeacon(GameObject obj)
    {
        if (beacons.Count > 0)
        {
            for (int i = 0; i < beacons.Count; i++) //prevenção contra nomes iguais
            {
                if (beacons[i].gameObject.name == obj.name)
                {
                    obj.name += " *";
                    obj.GetComponent<Beacon>().nome += " *";
                }
            }
        }
        beacons.Add(obj); //adiciona o beacon na lista, com o nome corretamente ajustado
    }
}
