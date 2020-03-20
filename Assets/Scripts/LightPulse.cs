using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UnityEngine.Experimental.Rendering.Universal
{
    public class LightPulse : MonoBehaviour
    {
        Light2D lightt;
        Sequence emitPulse;
        void Start()
        {
            emitPulse = DOTween.Sequence();
            lightt = GetComponent<Light2D>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                emitPulse.Kill();
                ChangeRadius(0);
                ChangeIntensity(2.5f);
                emitPulse.Append(DOVirtual.Float(0, 10, 0.3f, ChangeRadius).SetEase(Ease.InSine));
                emitPulse.AppendInterval(3);
                emitPulse.Append(DOVirtual.Float(2.5f, 0, 1, ChangeIntensity));
            }
        }

        void ChangeRadius(float f)
        {
            lightt.pointLightInnerRadius = f - 1;
            lightt.pointLightOuterRadius = f;
        }

        void ChangeIntensity(float f)
        {
            lightt.intensity = f;
        }
    }
}
