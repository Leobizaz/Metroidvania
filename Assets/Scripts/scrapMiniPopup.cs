using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrapMiniPopup : MonoBehaviour
{
    public Text display;
    public string value;

    private void Start()
    {
        Destroy(gameObject, 3);
        display.text = "<b>+</b> " + value;
    }



}
