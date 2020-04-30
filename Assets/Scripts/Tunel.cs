using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunel : MonoBehaviour
{
   // public GameObject txt;
    public GameObject Player;
    public Transform target;

    void Start()
    {
      //  txt.SetActive(false);  //////// N sei se vai precisar de instruções
    }

    void OnTriggerStay2D(Collider2D other)
    {
       // txt.SetActive(true);
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            print("Animação XD");
            PlayerController.OnMovement = false;
            ////Tocar Animação e Som/////
            Invoke("Teleport", 2f);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
       // txt.SetActive(false);
    }
    void Teleport()
    {
        Player.transform.position = target.transform.position;
        Invoke("UnlockMove", 2f);
    }
    void UnlockMove()
    {
        PlayerController.OnMovement = true;
    }
}
