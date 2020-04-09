using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace UnityEngine.Experimental.Rendering.Universal
{
    public class TriggerCogumelo : MonoBehaviour
    {
        public bool startOn;
        public bool canBeTriggered = true;
        public bool fades = true;
        public float fadeOutTime = 8;
        public float divisionValue = 3;
        public float activationTime = 3;
        public Light2D light2d;
        public TriggerCogumelo[] chain_reaction;
        public float chainDelay = 0;
        [HideInInspector] public GameObject ignore;
        bool fading;
        bool isfading;
        bool triggered;
        Material mat;
        Color def_color;
        private float def_lightValue;

        void Start()
        {
            mat = GetComponent<SpriteRenderer>().material;
            def_color = mat.GetColor("_ColorEmissive");
            mat.SetColor("_ColorEmissive", def_color / divisionValue);
            def_lightValue = light2d.intensity;
            light2d.intensity = 0;
            if (startOn) Trigger();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Light" && canBeTriggered)
            {
                CancelFade();
                Trigger();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Light" && canBeTriggered)
            {
                CancelFade();
                Trigger();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Light" && triggered)
            {
                CancelInvoke("ChainFadeOut");
                CancelInvoke("FadeOut");
                //Invoke("ChainFadeOut", 8f);
                Invoke("FadeOut", fadeOutTime);
            }
        }

        public void Trigger()
        {
            if (!triggered)
            {
                DOTween.Kill(gameObject);
                triggered = true;
                DOVirtual.Float(divisionValue, 1, activationTime, ChangeProperty);
                DOVirtual.Float(0, def_lightValue, activationTime, ChangeLight);
                if (chain_reaction.Length != 0)
                {
                    Invoke("TriggerChain", chainDelay);
                }
                if (fades)
                {
                    Invoke("FadeOut", fadeOutTime);
                }
            }

        }

        public void ChainFadeOut()
        {
            isfading = true;
            if (chain_reaction.Length != 0)
            {
                foreach (TriggerCogumelo cogumelo in chain_reaction)
                {
                    if (cogumelo.isfading) return;
                    cogumelo.FadeOut();
                }
            }
        }

        public void CancelFade()
        {
            fading = true;
            CancelInvoke("FadeOut");
            //CancelInvoke("CancelFading");
            //Invoke("CancelFading", 1f);
            /*
            if(chain_reaction.Length != 0)
            {
                foreach (TriggerCogumelo cogumelo in chain_reaction)
                {
                    if (!cogumelo.fading)
                    {
                        cogumelo.CancelFade();
                    }
                }
            }
            */
            
            Invoke("FadeOut", fadeOutTime);
        }

        void CancelFading()
        {
            fading = false;
        }

        public void TriggerChain()
        {
            foreach (TriggerCogumelo cogumelo in chain_reaction)
            {
                cogumelo.Trigger();
            }
        }

        void ChangeLight(float v)
        {
            light2d.intensity = v;
        }

        void ChangeProperty(float m)
        {
            mat.SetColor("_ColorEmissive", def_color / m);
        }

        void FadeOut()
        {
            fading = false;
            DOVirtual.Float(1, divisionValue, activationTime, ChangeProperty);
            DOVirtual.Float(def_lightValue, 0, activationTime, ChangeLight);
            triggered = false;
        }
    }
}
