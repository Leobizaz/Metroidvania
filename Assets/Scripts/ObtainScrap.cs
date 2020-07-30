using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainScrap : MonoBehaviour
{
    public GameObject indicator;
    public ParticleSystem shineFX;
    public GameObject scrapMiniPopup;
    public GameObject popupPlacement;
    public float animduration;
    public int divisionvalue = 1;
    int index;
    bool onArea;
    bool once;

    public float scrapAmmount;

    void Start()
    {
        indicator.SetActive(false);
    }
    void Update()
    {
        if (onArea && Input.GetKeyDown(KeyCode.E) && !once)
        {
            once = true;
            Execute();
            indicator.SetActive(false);
        }
    }

    public void Execute()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponentInChildren<Animator>().Play("Bob_Loot");
        player.GetComponentInChildren<Animator>().SetBool("IsLooting", true);
        player.GetComponent<PlayerController>().InteractPause(animduration + 0.5f);
        Invoke("StopAnimation", animduration);
        StartCoroutine(Popups());

        Invoke("UpdateValue", animduration + 0.5f);
        shineFX.Stop();
    }

    void StopAnimation()
    {
        GameObject.Find("Player").GetComponentInChildren<Animator>().SetBool("IsLooting", false);
    }

    IEnumerator Popups()
    {
        while(index < divisionvalue)
        {
            index++;

            var popup = Instantiate(scrapMiniPopup, popupPlacement.transform);
            popup.GetComponent<scrapMiniPopup>().value = Mathf.Round(scrapAmmount / divisionvalue).ToString();

            yield return new WaitForSeconds(animduration / (animduration - 0.5f));
        }
    }

    void UpdateValue()
    {
        GameEvents.current.UpdateScrap(GameController.currentScrap + scrapAmmount, false);
        Destroy(popupPlacement.transform.parent.gameObject);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !once)
        {
            onArea = true;
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            indicator.SetActive(false);
            onArea = false;
        }
    }
}
