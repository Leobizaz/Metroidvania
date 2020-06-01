using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCollectedLogs : MonoBehaviour
{
    public static int logsCollected;
    Text texto;

    private void Awake()
    {
        texto = GetComponent<Text>();
    }

    private void OnEnable()
    {
        texto.text = logsCollected + "/3";
    }

}
