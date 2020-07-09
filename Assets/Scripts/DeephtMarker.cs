using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeephtMarker : MonoBehaviour
{

    public GameObject Player;
    public GameObject Marker;
    public TextMeshProUGUI Valor;
    private float Dist;
    private float PrintDist;

   
    void Update()
    {
        Marker.transform.position = new Vector3(Player.transform.position.x, Marker.transform.position.y, Marker.transform.position.z);
        Dist = Vector2.Distance(Player.transform.position, Marker.transform.position);
        PrintDist = Dist / 4;
        Valor.text = " " + (Mathf.Round(PrintDist)) + "m";
    }
}
