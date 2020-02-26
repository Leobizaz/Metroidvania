using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanternController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject pivot;
    bool facingLeft;
    bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = pivot.transform.position;

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float finalRotz = 0;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        if (playerMovement.rb.velocity.x > 0)
            FacingLeft();

        if (playerMovement.rb.velocity.x < 0)
            FacingRight();

        transform.eulerAngles = new Vector3(0f, 0f, rot_z - 90);

    }

    void FacingLeft()
    {
        facingLeft = true;
        facingRight = false;
    }

    void FacingRight()
    {
        facingRight = true;
        facingLeft = false;
    }
}
