using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveCartao : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().unlockedCartao = true;
        Destroy(gameObject, 8);
    }
}
