using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class PlantaCresce : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer parteQbrilha;
    Material brilho;
    public Light2D luz;
    float initialLight;
    bool grown;
    Color initialColor;
    Sequence brilhoSequence;
    Sequence luzSequence;
    public string animationString;

    private void Awake()
    {
        initialLight = luz.intensity;
        brilhoSequence = DOTween.Sequence();
        luzSequence = DOTween.Sequence();
        brilho = parteQbrilha.material;
        initialColor = brilho.GetColor("_ColorEmissive");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Light" && !grown)
        {
            Debug.Log("Light");
            Wiggle();
        }

        if(collision.tag == "Flash")
        {
            if (!grown)
            {
                Grow();
                return;
            }
            CancelInvoke("UnGrow");
            Invoke("UnGrow", 13f);
        }
    }


    void Grow()
    {
        luzSequence.Kill();
        brilhoSequence.Kill();
        luzSequence.Append(DOVirtual.Float(initialLight, 1.3f, 3, ChangeLight));
        brilhoSequence.Append(DOVirtual.Float(1, 3, 3, ChangeColor));
        grown = true;
        Invoke("GrowAnim", 0.3f);
        Invoke("UnGrow", 13f);
    }

    void GrowAnim()
    {
        anim.Play(("Grow" + animationString));
    }

    void Wiggle()
    {
        luzSequence.Kill();
        brilhoSequence.Kill();
        luzSequence.Append(DOVirtual.Float(initialLight, 1, 3, ChangeLight));
        luzSequence.PrependInterval(3);
        brilhoSequence.Append(DOVirtual.Float(1, 2, 3, ChangeColor));
        brilhoSequence.PrependInterval(3);
        luzSequence.Append(DOVirtual.Float(1, initialLight, 3, ChangeLight));
        brilhoSequence.Append(DOVirtual.Float(2, 1, 3, ChangeColor));
        brilhoSequence.PrependInterval(3);
        brilhoSequence.Kill();
        luzSequence.PrependInterval(3);
        luzSequence.Kill();
        anim.Play("Wiggle");
    }

    void DisableGrow()
    {
        grown = false;
    }

    void UnGrow()
    {
        DOVirtual.Float(1.3f, initialLight, 4, ChangeLight);
        DOVirtual.Float(3, 1, 4, ChangeColor);
        anim.Play(("UnGrow" + animationString));
        Invoke("DisableGrow", 1f);
    }

    void ChangeLight(float v)
    {
        luz.intensity = v;
    }

    void ChangeColor(float c)
    {
        brilho.SetColor("_ColorEmissive", initialColor * c);
    }
}
