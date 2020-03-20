using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public PlayerMovement movement;

    public float speed;
    void Update()
    {

        //////////////////Mouse/////////////////////////

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        ///////////////////Controle////////////////////

        //Vector3 control_pos = new Vector3(Input.GetAxis("Horizontal2") * 180f, Input.GetAxis("Vertical2") * 180f, 0f);

        //Vector3 diff = (-control_pos) - transform.position;

        //diff.Normalize();

        //diff.x = 0;
        //diff.z = 0;

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;


        float rot_z2 = Mathf.Atan2(-diff.y, -diff.x) * -Mathf.Rad2Deg;

        var zLock = Mathf.Clamp(rot_z, -90, 90);
        if (zLock < 0) zLock = 360 + zLock;
        var zLock2 = Mathf.Clamp(rot_z2, -90, 90);
        if (zLock2 < 0) zLock2 = 360 + zLock2;
        rot_z = zLock;
        rot_z2 = zLock2;


        if (PlayerMovement.facingright == true)
            transform.eulerAngles = new Vector3(0f, 0f, rot_z);
        if(PlayerMovement.facingleft == true)
            transform.eulerAngles = new Vector3(0f, 0f, -rot_z2);


    }
}
