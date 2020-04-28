﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.U2D;

public class HideSecret : MonoBehaviour
{
    public bool inverted;
    public float delayedTime;
    public float time;
    public SpriteShapeRenderer background;
    private Material bgMaterial;

    private void Awake()
    {
        bgMaterial = background.material;
    }

    public void DelayedUnhide()
    {
        Invoke("Unhide", delayedTime);
    }

    public void DelayedHide()
    {
        Invoke("Hide", delayedTime);
    }


    public void Unhide()
    {
        DOVirtual.Float(bgMaterial.color.a, 0, time, UpdateColor).SetEase(Ease.Linear);
    }

    public void Hide()
    {
        DOVirtual.Float(bgMaterial.color.a, 100, time, UpdateColor).SetEase(Ease.Linear);
    }

    void UpdateColor(float a)
    {
        bgMaterial.SetColor("_Color", new Color32(0, 0, 0, (byte)(a * 255)));
    }


}
