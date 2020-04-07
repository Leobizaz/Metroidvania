using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstalactiteChild : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            this.rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
