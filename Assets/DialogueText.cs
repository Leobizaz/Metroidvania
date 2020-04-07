using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText : MonoBehaviour
{
    public string text;
    public float duration;
    public bool newLine;
    public bool replace;
    public bool instantReplace;
    public float replaceDuration;
    public bool playNext;
    public float timeForNext;
    public AudioClip audio;
    public bool audioloop;
    public int eventID;
}
