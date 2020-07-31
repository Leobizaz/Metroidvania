using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrapgate : MonoBehaviour
{
    public int scrapGate_id;

    public GameObject indicatorFix;
    public GameObject indicatorRequer;
    public Text indicatorFixCounter;
    public Text indicatorRequerCounter;

    public GameObject spriteFixed;
    public GameObject spriteBroken;

    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;

    public float scrapRequired;
    bool gatefixed;

    bool onArea;
    bool once;

    void Start()
    {
        indicatorFix.SetActive(false);
        indicatorRequer.SetActive(false);

        indicatorFixCounter.text = scrapRequired.ToString();
        indicatorRequerCounter.text = scrapRequired.ToString();

    }

    void Update()
    {
        if (onArea && Input.GetKeyDown(KeyCode.E) && !once && GameController.currentScrap >= scrapRequired)
        {
            once = true;
            Execute();
            indicatorFix.SetActive(false);
            indicatorRequer.SetActive(false);
        }
    }

    public void Execute()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().PausePlayer();
        ControllableFade.current.Fadeout(4);
        Invoke("DeactivateObjects", 2);
        Invoke("ActivateObjects", 2);
        Invoke("ReleasePlayer", 4.9f);
        gatefixed = true;
    }

    public void DeactivateObjects()
    {
        for (int i = 0; i < objectsToDeactivate.Length; i++)
        {
            objectsToDeactivate[i].SetActive(false);

        }
    }

    public void ActivateObjects()
    {
        spriteBroken.SetActive(false);
        spriteFixed.SetActive(true);

        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(true);

        }

    }

    public void ReleasePlayer()
    {
        GameEvents.current.UpdateScrap(GameController.currentScrap - scrapRequired, true);
        GameObject.Find("Player").GetComponent<PlayerController>().UnPausePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !gatefixed)
        {

            ScrapDisplayer.current.DisplayScrap();

            onArea = true;
            if (GameController.currentScrap >= scrapRequired)
            {
                indicatorFix.SetActive(true);
                indicatorRequer.SetActive(false);
            }
            else
            {
                indicatorRequer.SetActive(true);
                indicatorFix.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            indicatorFix.SetActive(false);
            indicatorRequer.SetActive(false);
            onArea = false;
        }
    }
}
