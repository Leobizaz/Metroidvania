using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public string Name;
    public GameObject inputField;
    public GameObject textDisplay;
    public GameObject button;
    public static Transform GUIName;

    public GameObject Compass;
    public GameObject Markerpannel;
    public GameObject target;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && Time.timeScale == 1) 
            {
                Markerpannel.SetActive(true);
                Time.timeScale = 0;
            }
        if (Input.GetKeyDown(KeyCode.Tab) && Time.timeScale == 0 && Markerpannel == true)
        {
            Markerpannel.SetActive(false);
            Time.timeScale = 1;
        }

    }

    public void StoreName()
    {
        Name = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent < Text >().text = Name;
        target.gameObject.name = Name;
        Instantiate(target, Player.position, Player.rotation);
        GUIName = target.transform;
        Compass.SetActive(true);


    }
}
