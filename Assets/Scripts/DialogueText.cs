using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DialogueText : MonoBehaviour
{
    public string textPT;
    public string textES;
    public string textEN;
    public float duration;
    public bool newLine;
    public bool replace;
    public bool instantReplace;
    public float replaceDuration;
    public bool scramble;
    public bool playNext;
    public float timeForNext;
    public AudioClip audio;
    public bool audioloop;
    public int eventID;
    public string text;

    private void Update()
    {
      if(Lean.Localization.LeanLocalization.CurrentLanguage == "Portuguese")
        {
            text = textPT;
        }
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "Spanish")
        {
            text = textES;
        }
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
            text = textEN;
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
            gameObject.name = text;
    }

}



