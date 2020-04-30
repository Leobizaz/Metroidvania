using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UnityEngine.Experimental.Rendering.Universal
{
    public class FlashFade : MonoBehaviour
    {
        public bool startOn;
        public float duration;
        float def_lightValue;
        Light2D light2d;
        public BoxCollider2D residual;
        PolygonCollider2D colliderr;
        AudioSource audioS;

        private void Awake()
        {
            light2d = GetComponent<Light2D>();
            audioS = GetComponent<AudioSource>();
            colliderr = GetComponent<PolygonCollider2D>();
            def_lightValue = light2d.intensity;
        }

        private void Start()
        {
            light2d.enabled = true;

            if (startOn)
                Trigger();
            else
            {
                light2d.intensity = 0;
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            Trigger();
        }

        public void Trigger()
        {
            GameEvents.current.MakeSound(gameObject);
            CancelInvoke("FadeCollider");
            light2d.intensity = def_lightValue;
            audioS.Play();
            colliderr.enabled = true;
            residual.enabled = true;
            DOVirtual.Float(def_lightValue, 0, duration, ChangeLight).SetEase(Ease.OutSine);
            Invoke("FadeCollider", duration);
        }

        void FadeCollider()
        {
            colliderr.enabled = false;
            residual.enabled = false;
            gameObject.SetActive(false);
        }

        void ChangeLight(float v)
        {
            light2d.intensity = v;
        }

    }

}
