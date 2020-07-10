using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad : MonoBehaviour
{
    public static bool playerHasDiedOnce;

    public GameObject desativaIntro;
    public GameObject[] objectsToDeactivate;
    public GameObject[] objectsToActivate;


    private void Awake()
    {
        playerHasDiedOnce = true;
    }

    private void Start()
    {
        if (playerHasDiedOnce)
        {
            for(int i = 0; i < objectsToDeactivate.Length; i++)
            {
                objectsToDeactivate[i].SetActive(false);
            }

            for (int i = 0; i < objectsToActivate.Length; i++)
            {
                objectsToActivate[i].SetActive(true);
            }
        }
    }
}
