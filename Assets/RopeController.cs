using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    private GameObject player;
    public GameObject ExitTOP;
    public GameObject ExitBOT;
    public GameObject TriggerTOP;
    public GameObject TriggerBOT;

    [SerializeField] bool mouseOver;
    [SerializeField] bool playerOnRope;

    void Start()
    {
        player = GameObject.Find("Character");
        DeActivateTriggers();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && playerOnRope)
        {
            ClimbOff();
        }

        if (mouseOver && Input.GetKeyDown(KeyCode.E) && !playerOnRope)
        {
            ClimbOn();
        }

    }

    public void ClimbOn()
    {
        Invoke("ActivateTriggers", 3f);
        playerOnRope = true;
        Vector3 newPos = new Vector3(this.transform.position.x, player.transform.position.y, player.transform.position.z);
        player.transform.position = newPos;
        player.transform.parent = this.gameObject.transform;
        player.GetComponent<PlayerMovement>().ClimbRope();

    }

    public void ClimbOff()
    {
        Invoke("DeActivateTriggers", 1f);
        playerOnRope = false;
        player.transform.parent = null;
        player.GetComponent<PlayerMovement>().DismountRope();
    }

    private void OnMouseOver()
    {
        if (Vector2.Distance(player.transform.position, TriggerBOT.transform.position) < 3 || Vector2.Distance(player.transform.position, TriggerTOP.transform.position) < 3 || Vector2.Distance(player.transform.position, this.transform.position) < 3)
            mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }

    public void ActivateTriggers()
    {
        TriggerBOT.SetActive(true);
        TriggerTOP.SetActive(true);
    }


    public void DeActivateTriggers()
    {
        TriggerBOT.SetActive(false);
        TriggerTOP.SetActive(false);
    }
}
