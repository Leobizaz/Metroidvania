using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DialogueText : MonoBehaviour
{
    public string text;
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
#if UNITY_EDITOR
    private void Update()
    {
        if(Application.platform == RuntimePlatform.WindowsEditor)
            gameObject.name = text;
    }
#endif
}



