using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabJetpackAnimation : MonoBehaviour
{
    Animator anim;
    PlayerController player;

    private void Awake()
    {
        player = transform.parent.GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }
    public void Play()
    {
        transform.parent.transform.position = new Vector3(-57.953f, -322.952f, 0);
        transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.Freio();

        if (PlayerController.facingleft)
            player.FaceRight();

        player.isBusy = true;
        anim.Play("PickupJetpack");
    }

    public void FinishJetpackAnimation()
    {
        player.isBusy = false;
    }

    public void JetpackFadeOut()
    {
        ControllableFade.current.Fadeout(1);
    }



}
