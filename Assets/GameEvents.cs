using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;


    private void Awake()
    {
        current = this;
    }

    public event Action<GameObject> onMakeSound;
    public event Action<GameObject> onMakeBigSound;

    public void MakeSound(GameObject obj)
    {
        if(onMakeSound != null)
        {
            onMakeSound(obj);
        }
    }

    public void MakeBigSound(GameObject obj)
    {
        if(onMakeBigSound != null)
        {
            onMakeBigSound(obj);
        }
    }
}
