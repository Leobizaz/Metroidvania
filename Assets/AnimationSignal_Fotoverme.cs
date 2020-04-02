using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSignal_Fotoverme : MonoBehaviour
{
    public Fotoverme fotoverme;

    public void Transition()
    {
        fotoverme.Charge();
    }

    public void Grunt()
    {
        fotoverme.Grunt();
    }

    public void Come()
    {
        fotoverme.Come();
    }

    public void Die()
    {
        fotoverme.Die();
    }
}
