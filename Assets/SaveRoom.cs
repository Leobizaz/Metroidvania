using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveRoom : MonoBehaviour
{
    public int health;
    public float[] position;
    public bool[] Logs;
    public bool beacon;
    public bool flash;
    public bool jetpack;

    public bool reator;

    public GameObject triggerFlash;
    public GameObject triggerMing;
    public GameObject triggerJosh;
    public bool Ming;
    public bool Josh;

    public SaveRoom (PlayerController player)
    {
        health = PlayerController.hits;
        beacon = player.unlockedBeacon;
        flash = player.unlockedFlash;
        jetpack = player.unlockedJetpack;

        if (Ming == true)
            Destroy(triggerMing);
        if (Josh == true)
            Destroy(triggerJosh);

        reator = ReatorOnEnable.ReatorOn;
        

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        Logs = new bool[3];
        Logs[0] = UnlockLog.Log[0];
        Logs[1] = UnlockLog.Log[1];
        Logs[2] = UnlockLog.Log[2];
    }
}
