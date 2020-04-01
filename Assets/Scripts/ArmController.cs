using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArmController : MonoBehaviour
{
    public float speed;
    public GameObject gun;
    bool once;
    bool once2;
    bool once3;
    bool once4;
    Sequence sequence;

    private void Start()
    {
        sequence = DOTween.Sequence();
    }

    void LateUpdate()
    {

        //////////////////Mouse/////////////////////////

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        ///////////////////Controle////////////////////

        //Vector3 control_pos = new Vector3(Input.GetAxis("Horizontal2") * 180f, Input.GetAxis("Vertical2") * 180f, 0f);

        //Vector3 diff = (-control_pos) - transform.position;

        //diff.Normalize();

        //diff.x = 0;
        //diff.z = 0;

        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos -= new Vector3(0.5f, 0.5f, 0.0f) * 1;

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;


        float rot_z2 = Mathf.Atan2(-diff.y, -diff.x) * -Mathf.Rad2Deg;

        var zLock = Mathf.Clamp(rot_z, -90, 90);
        if (zLock < 0) zLock = 360 + zLock;
        var zLock2 = Mathf.Clamp(rot_z2, -90, 90);
        if (zLock2 < 0) zLock2 = 360 + zLock2;
        rot_z = zLock;
        rot_z2 = zLock2;


        if (PlayerController.facingright == true)
        {
            if (!once)
            {
                once = true;
                once2 = false;
                DOTween.Kill(transform);
            }

            if (mousePos.x < 0)
            {
                if (!once3)
                {
                    once3 = true;
                    if (mousePos.y > 0)
                    {
                        transform.DOLocalRotate(new Vector3(0, 0, -135), 1.0f, RotateMode.FastBeyond360).SetRelative();
                    }
                    else
                    {
                        transform.DOLocalRotate(new Vector3(0, 0, 45), 0.3f, RotateMode.FastBeyond360).SetRelative();
                    }
                }

                //UpdateRotation();
            }
            else
            {
                DOTween.Kill(transform);
                once3 = false;
                transform.localEulerAngles = new Vector3(0f, 180f, rot_z);
            }
        }
        if (PlayerController.facingleft == true)
        {

            if (!once2)
            {
                once2 = true;
                once = false;
                DOTween.Kill(transform);
            }
            if (mousePos.x > 0)
            {
                if (!once4)
                {
                    once4 = true;
                    if (mousePos.y > 0)
                    {
                        transform.DOLocalRotate(new Vector3(0, 0, -135), 1.0f, RotateMode.FastBeyond360).SetRelative();
                    }
                    else
                    {
                        transform.DOLocalRotate(new Vector3(0, 0, 45), 0.3f, RotateMode.FastBeyond360).SetRelative();
                        //transform.DOLocalRotate(new Vector3(180, 0, 135f), 1);
                    }
                    //DOTween.To(UpdateRotation, transform.localRotation.z, 135f, 1);

                    //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(180f, 0f, 135f), 1);
                }
                //UpdateRotation();
            }
            else
            {
                DOTween.Kill(transform);
                once4 = false;
                transform.localEulerAngles = new Vector3(0f, 180f, rot_z2);
            }
        }


    }

    public void UpdateRotation(float z)
    {
        transform.localEulerAngles = new Vector3(180, 0, z);
    }
}
