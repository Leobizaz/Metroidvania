using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstalactiteChild : MonoBehaviour
{
    public Rigidbody2D rb;
    AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            audioS.Play();
            this.rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
