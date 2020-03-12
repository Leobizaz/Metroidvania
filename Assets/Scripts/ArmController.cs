using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public PlayerMovement movement;

    public float speed;
    void Update()
    {

        
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        //diff.x = 0;
        //diff.z = 0;

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0f, 0f, rot_z);
        

        //transform.Rotate(new Vector3(0f, 0f, Input.GetAxis("Mouse Y")) * Time.deltaTime * speed);
        //transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(transform.rotation.z, 0, 90));

    }
}
