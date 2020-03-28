using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFake : MonoBehaviour
{
    public MobDetect detector;
    public DriveSpline driver;
    public Animator anim;
    AudioSource audioS;
    public SpriteRenderer spr_renderer;
    bool detected;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(detector.detected && !detected)
        {
            detected = true;
            audioS.Play();
            Invoke("MoveAway", 1f);
        }
    }

    void MoveAway()
    {
        spr_renderer.flipX = true;
        anim.Play("spider_moving");
        driver.move = true;
    }


}
