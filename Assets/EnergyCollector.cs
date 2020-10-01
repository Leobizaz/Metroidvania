using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class EnergyCollector : MonoBehaviour
{
    public static EnergyCollector current;
    public float shieldcharge;
    public Image barraShield;
    public GameObject dialogoShield;
    bool once;

    private void Awake()
    {
        current = this;
    }

    private void Update()
    {
        barraShield.fillAmount = Mathf.InverseLerp(0, 100, shieldcharge);

        if(shieldcharge >= 20)
        {
            barraShield.color = new Color32(255, 255, 255, 100);
        }
        else
        {
            barraShield.color = new Color32(142, 142, 142, 100);
        }



    }

    public void ChargeCollector()
    {
        if (!once)
        {
            dialogoShield.SetActive(true);
            once = true;
        }

        shieldcharge++;
        if (shieldcharge > 100) shieldcharge = 100;
    }

    public void GetHit(float value)
    {
        DOVirtual.Float(shieldcharge, shieldcharge - value, 0.5f, UpdateValue);
    }

    void UpdateValue(float v)
    {
        shieldcharge = v;
        if (shieldcharge < 0) shieldcharge = 0;
    }
}
