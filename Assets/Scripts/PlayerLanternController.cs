using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerLanternController : MonoBehaviour
{
    public GameObject pivot;
    bool once;
    bool once2;
    bool once3;
    bool once4;
    public GameObject child;
    Vector3 childScale;
    Sequence sequence;
    public PolygonCollider2D lightCol;
    // Start is called before the first frame update
    void Start()
    {
        childScale = child.transform.localScale;
        sequence = DOTween.Sequence();
    }

    private void LateUpdate()
    {
        this.transform.position = pivot.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        /*
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0f, 0f, rot_z - 90);
        */ // 360 graus


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
                lightCol.enabled = false;
                Invoke("ResetCollider", 0.2f);
                DOTween.Kill(gameObject);
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 180);
                //transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + 180), 0.3f);
                once = true;
                once2 = false;
                once3 = true;
            }
            if (once)
            {
                child.transform.localScale = new Vector3(childScale.x, -childScale.y, childScale.z);
                if (mousePos.x < 0)
                {
                    if (!once3)
                    {
                        once3 = true;
                        if (mousePos.y > 0)
                        {
                            sequence.Append(transform.DOLocalRotate(new Vector3(0, 0, -135), 1.0f, RotateMode.FastBeyond360).SetRelative());
                        }
                        else
                        {
                            sequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 45), 0.3f, RotateMode.FastBeyond360).SetRelative());
                        }
                    }

                    //UpdateRotation();
                }
                else
                {
                    DOTween.Kill(gameObject);
                    once3 = false;
                    transform.eulerAngles = new Vector3(180f, 180f, rot_z);
                }
            }
        }
        if (PlayerController.facingleft == true)
        {
            if (!once2)
            {
                lightCol.enabled = false;
                Invoke("ResetCollider", 0.3f);
                once2 = true;
                once = false;
                once4 = true;
                DOTween.Kill(gameObject);
                transform.eulerAngles = new Vector3(0, 180, transform.eulerAngles.z - 180);
                //transform.DORotate(new Vector3(0, 180, transform.eulerAngles.z - 180), 0.3f);
            }
            if (once2)
            {
                child.transform.localScale = childScale;
                if (mousePos.x > 0)
                {
                    if (!once4)
                    {
                        once4 = true;
                        if (mousePos.y > 0)
                        {
                            sequence.Append(transform.DOLocalRotate(new Vector3(0, 0, -135), 1.0f, RotateMode.FastBeyond360).SetRelative());
                        }
                        else
                        {
                            sequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 45), 0.3f, RotateMode.FastBeyond360).SetRelative());
                            //transform.DOLocalRotate(new Vector3(180, 0, 135f), 1);
                        }
                        //DOTween.To(UpdateRotation, transform.localRotation.z, 135f, 1);

                        //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(180f, 0f, 135f), 1);
                    }
                    //UpdateRotation();
                }
                else
                {
                    DOTween.Kill(gameObject);
                    once4 = false;
                    transform.localEulerAngles = new Vector3(0f, 180f, rot_z2);
                }
            }
        }

    }

    void ResetCollider()
    {
        lightCol.enabled = true;
    }
}
