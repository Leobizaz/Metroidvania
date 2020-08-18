using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveRoom : MonoBehaviour
{
    public int health;
    public float[] position;
    public bool beacon;
    public bool flash;
    public bool jetpack;
    
    public SaveRoom (PlayerController player)
    {
        health = PlayerController.hits;
        beacon = player.unlockedBeacon;
        flash = player.unlockedFlash;
        jetpack = player.unlockedJetpack;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
