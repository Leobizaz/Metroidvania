using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UnityEngine.Experimental.Rendering.Universal
{
    public class LightPulse : MonoBehaviour
    {
        Light2D light;
        Sequence emitPulse;
        void Start()
        {
            emitPulse = DOTween.Sequence();
            light = GetComponent<Light2D>();
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
            light.pointLightInnerRadius = f - 1;
            light.pointLightOuterRadius = f;
        }

        void ChangeIntensity(float f)
        {
            light.intensity = f;
        }
    }
}
