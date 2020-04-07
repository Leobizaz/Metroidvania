using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjects : MonoBehaviour
{
    public GameObject[] objects;
    public bool onStartup;
    public float timeBetweenObjects;
    public float delay;
    public bool automatic;
    int i;

    private void OnEnable()
    {
        if (onStartup)
        {
            Invoke("Enable", delay);
        }
    }

    public void Enable()
    {
        if (objects.Length > i)
        {
            objects[i].SetActive(true);
            i++;
            if (automatic)
                Invoke("Enable", timeBetweenObjects);
        }
    }

    public void EnableSpecific(int a)
    {
        if (objects[a] != null)
        {
            objects[a].SetActive(true);
        }
    }
}
