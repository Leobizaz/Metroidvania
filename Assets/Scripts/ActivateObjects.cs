using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjects : MonoBehaviour
{
    public GameObject[] objects;
    public bool onStartup;
    public float timeBetweenObjects;
    public bool deActivate = false;
    public float delay;
    public bool automatic;
    int i;

    private void OnEnable()
    {
        if (onStartup)
        {
            if (deActivate)
            {
                Invoke("Disable", delay);
                return;
            }
            Invoke("Enable", delay);
        }
    }

    public void Disable()
    {
        if (objects.Length > i)
        {
            objects[i].SetActive(false);
            i++;
            if (automatic)
                Invoke("Disable", timeBetweenObjects);
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
