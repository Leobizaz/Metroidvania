using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace UnityEngine.Experimental.Rendering.Universal
{
    public class TriggerCogumelo : MonoBehaviour
    {
        public SpriteRenderer spr_renderer;
        public bool startOn;
        public bool canBeTriggered = true;
        public bool fades = true;
        public bool fadeOnTrigger = false;
        public float fadeOutTime = 8;
        public float divisionValue = 3;
        public float activationTime = 3;
        public Light2D light2d;
        public AudioClip[] sons;
        AudioSource audioS;
        public TriggerCogumelo[] chain_reaction;
        public float chainDelay = 0;
        [HideInInspector] public GameObject ignore;
        bool fading;
        bool isfading;
        bool triggered;
        Material mat;
        public Color def_color;
        public Fotoverme[] vermesPraAcordar;
        private float def_lightValue;

        void Start()
        {
            mat = spr_renderer.material;
            if(GetComponent<AudioSource>())
                audioS = GetComponent<AudioSource>();
            def_color = mat.GetColor("_ColorEmissive");
            mat.SetColor("_ColorEmissive", new Color32(0,0,0,0));
            def_lightValue = light2d.intensity;
            light2d.intensity = 0;
            if (startOn) Trigger();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(fadeOnTrigger && collision.tag == "Player")
            {
                CancelFade();
                Trigger();
                return;
            }

            if (collision.tag == "Light" && canBeTriggered)
            {
                CancelFade();
                Trigger();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (fadeOnTrigger) return;
            if (collision.tag == "Light" && canBeTriggered)
            {
                CancelFade();
                Trigger();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(fadeOnTrigger && collision.tag == "Player")
            {
                TriggeredFadeOut();
                return;
            }

            if (collision.tag == "Light" && triggered)
            {
                CancelInvoke("ChainFadeOut");
                CancelInvoke("FadeOut");
                //Invoke("ChainFadeOut", 8f);
                if(!canBeTriggered)
                    Invoke("FadeOut", fadeOutTime);
            }
        }

        public void Trigger()
        {
            if (!triggered)
            {
                if (light2d.gameObject.GetComponent<CircleCollider2D>())
                {
                    CircleCollider2D lightCol = light2d.gameObject.GetComponent<CircleCollider2D>();
                    lightCol.enabled = true;
                    lightCol.radius = lightCol.radius - 2;
                    lightCol.radius = lightCol.radius + 2;
                }

                if(vermesPraAcordar.Length > 0)
                {
                    for(int i = 0; i < vermesPraAcordar.Length; i++)
                    {
                        vermesPraAcordar[i].GetDetected();
                    }
                }

                if (GetComponent<AudioSource>() && sons.Length > 0)
                    PlayOneShotRandom(sons);

                DOTween.Kill(gameObject);
                triggered = true;
                //DOVirtual.Float(divisionValue, 1, activationTime, ChangeProperty);
                mat.DOColor(def_color, "_ColorEmissive" , activationTime);
                DOVirtual.Float(0, def_lightValue, activationTime, ChangeLight);
                Invoke("LightTriggerOn", activationTime);
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

        public void PlayOneShotRandom(AudioClip[] audios)
        {
            int i = Random.Range(0, audios.Length);
            audioS.PlayOneShot(audios[i]);

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
            //mat.SetColor("_ColorEmissive", new Color32(0,0,0,0));
        }

        void FadeOut()
        {
            if (fades)
            {
                fading = false;
                //DOVirtual.Float(1, divisionValue, activationTime, ChangeProperty);
                mat.DOColor(new Color32(0, 0, 0, 0), "_ColorEmissive", activationTime);
                DOVirtual.Float(def_lightValue, 0, activationTime, ChangeLight);
                triggered = false;
                Invoke("LightTriggerOff", activationTime);
            }
        }

        void TriggeredFadeOut()
        {
            fading = false;
            //DOVirtual.Float(1, divisionValue, activationTime, ChangeProperty);
            mat.DOColor(new Color32(0, 0, 0, 0), "_ColorEmissive", activationTime);
            DOVirtual.Float(def_lightValue, 0, activationTime, ChangeLight);
            triggered = false;
            Invoke("LightTriggerOff", activationTime);
        }

        void LightTriggerOff()
        {
            if(fades)
            light2d.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

        void LightTriggerOn()
        {
            if(!fadeOnTrigger)
            light2d.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
