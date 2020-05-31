using System.Collections;
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
    public GameObject fade;
    public bool chaseEvent;

    private void Start()
    {
        if (chaseEvent)
            GameEvents.current.onChaseReset += OnReset;

        

        background.gameObject.SetActive(true);
        bgMaterial = background.material;
        if (inverted) Unhide();
    }

    void OnReset()
    {
        bgMaterial.SetColor("_Color", new Color(0, 0, 0, 255));
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
        if (fade != null) fade.SetActive(false);
    }

    public void Hide()
    {
        DOVirtual.Float(bgMaterial.color.a, 255, time, UpdateColor).SetEase(Ease.Linear);
    }

    void UpdateColor(float a)
    {
        bgMaterial.SetColor("_Color", new Color32(0, 0, 0, (byte)a));
    }


}
