using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveRoom
{
    public int health;
    public int logNum;
    public float[] position;
    public float scrap;

    public bool[] Logs;
    public bool beacon;
    public bool flash;
    public bool jetpack;
    public bool knife;
    public bool reator;
    public bool Ming;
    public bool Josh;

    

    public SaveRoom (PlayerController player)
    {
        health = PlayerController.hits;
        beacon = player.unlockedBeacon;
        flash = player.unlockedFlash;
        jetpack = player.unlockedJetpack;
        knife = player.unlockedKnife;
        reator = ReatorOnEnable.ReatorOn;

        scrap = GameController.currentScrap;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        Ming = OnEnableMing.TriggerMingOn;
        Josh = OnEnableJo.TriggerJoshOn;

        if(Ming == true)
            LogUnlocker.current.UnlockLog(2);
        if (Josh == true)
            LogUnlocker.current.UnlockLog(1);
    }
}
