using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnimationEvents : MonoBehaviour
{
    public void Scream()
    {
        GameEvents.current.ShakeCamera(2.5f, 2.3f, 4.5f);
    }
}
