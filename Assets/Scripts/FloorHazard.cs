using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHazard : MonoBehaviour
{
    public Sprite catchSprite;
    private bool PlayerLocked;
    private int count = 13;
    AudioSource audioSource;

    void Start()
    {
        PlayerLocked = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        

        if(PlayerLocked == true)
        {
            if (count == 0)
            {
                Destroy(this.gameObject);
                count = 13;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                count -= 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                count -= 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = catchSprite; //Remover isso qundo ter a animação final
            PlayerLocked = true;
            audioSource.Play();
            PlayerController.hits = -1;
        }
    }

}
