using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialoguePlayer : MonoBehaviour
{
    Text textBox;
    AudioSource audioS;
    public DialogueText[] texts;
    public ActivateObjects objects;
    int id;
    Transform parent;
    string lastString;
    public AnimationCurve curveOpen;
    public AnimationCurve curveClose;
    public bool animate;
    public bool fadeOut;
    public Vector3 startScale;
    public Vector3 endScale;
    public float animationSpeed;

    private void OnEnable()
    {
        Initialize();
        if (animate)
        {
            parent.localScale = startScale;
            parent.DOScale(endScale, animationSpeed).SetEase(curveOpen);

        }
    }

    public void Initialize()
    {
        textBox = GetComponent<Text>();
        Invoke("PlayText", 1);
        parent = transform.parent.transform;
        id = 0;
        audioS = GetComponent<AudioSource>();
    }

    public void PlayText()
    {
        lastString = textBox.text;
        if(id >= texts.Length)
        {
            Close();
            return;
        }
        var dialogueText = texts[id];
        id++;
        if (dialogueText.replace)
        {
            if (dialogueText.instantReplace)
            {
                textBox.text = null;
                if(dialogueText.scramble)
                    textBox.DOText(dialogueText.text, dialogueText.duration, true, ScrambleMode.All).SetEase(Ease.Linear);
                else
                    textBox.DOText(dialogueText.text, dialogueText.duration, true).SetEase(Ease.Linear);
            }
            else
            {
                if(dialogueText.scramble)
                    textBox.DOText(dialogueText.text, dialogueText.replaceDuration, true, ScrambleMode.All).SetEase(Ease.Linear);
                else
                    textBox.DOText(dialogueText.text, dialogueText.replaceDuration, true).SetEase(Ease.Linear);
            }
        }
        else
        {
            if (dialogueText.newLine) lastString = lastString + System.Environment.NewLine;
            textBox.DOText(lastString + dialogueText.text, dialogueText.duration, true).SetEase(Ease.Linear);
        }
        if (dialogueText.audio != null)
        {
            audioS.clip = dialogueText.audio;
            if (dialogueText.audioloop)
            {
                audioS.loop = true;
                Invoke("StopAudio", dialogueText.duration);
            }
            else
                audioS.loop = false;

            audioS.Play();

        }

        if(dialogueText.eventID != 0)
        {
            PlayEvent(dialogueText.eventID);
        }

        if(dialogueText.playNext)
            Invoke("PlayText", dialogueText.duration + dialogueText.timeForNext);

    }

    public void PlayEvent(int id)
    {
        if(id == 1)
            objects.Enable();
        if (id == 2)
        {
            Invoke("EnableSpecific", 3);
        }
    }

    public void StopAudio()
    {
        audioS.Stop();
    }

    void EnableSpecific()
    {
        objects.EnableSpecific(2);
    }

    public void Close()
    {
        if (fadeOut)
        {
            textBox.DOColor(new Color32(255, 255, 255, 0), animationSpeed+2);
        }
        else
        {
            parent.DOScale(startScale, animationSpeed).SetEase(curveClose);
        }
        Destroy(parent.gameObject, animationSpeed + 3);
    }

}
