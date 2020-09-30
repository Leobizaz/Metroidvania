using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabKnifeAnimation : MonoBehaviour
{
    PlayerController player;
    void Start()
    {
        player = transform.parent.gameObject.GetComponent<PlayerController>();
    }

    public void Play()
    {
        transform.parent.transform.position = new Vector3(-45.755f, -286.803f, 0);
        transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.Freio();

        if (PlayerController.facingleft)
            player.FaceRight();

        player.isBusy = true;
        GetComponent<Animator>().Play("KnifeCutscene");
    }

    public void FinishAnimationFaca()
    {
        player.isBusy = false;
    }

}
