using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconPlacer : MonoBehaviour
{
    public GameObject beaconPrefab;
    public Transform placement;
    public PromptNome promptNome;
    PlayerController playerControl;
    public int beaconCount;

    private void Start()
    {
        GameEvents.current.onRemoveBeacon += BeaconRemoved;
        playerControl = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!playerControl.isBusy && !promptNome.nomeando && playerControl.unlockedBeacon)
            {
                if(beaconCount < 4)
                    TryPlaceBeacon();
                else
                {
                    //n vai dar n
                }
            }
        }
    }

    void BeaconRemoved(GameObject obj)
    {
        beaconCount--;
    }

    public void TryPlaceBeacon()
    {
        promptNome.StartNaming(); //ver o código "PromptNome.cs"
    }

    public void PlaceBeacon(string nome) //recebe o nome através de "PromptNome.cs"
    {
        beaconCount++;
        var newBeacon = Instantiate(beaconPrefab, placement.position, Quaternion.identity);
        newBeacon.GetComponent<Beacon>().nome = nome;
    }

}
