using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;

namespace UnityEngine.Experimental.Rendering.Universal
{
    public class ArmController : MonoBehaviour
    {
        public float speed;
        public GameObject gun;
        bool once;
        bool once2;
        bool once3;
        bool once4;
        Sequence sequence;
        Quaternion targetRotation;

        private void Start()
        {
            sequence = DOTween.Sequence();
            targetRotation = Quaternion.Euler(0, 0, -135);
        }

        void LateUpdate()
        {

            //////////////////Mouse/////////////////////////


            ///////////////////Controle////////////////////

            //Vector3 control_pos = new Vector3(Input.GetAxis("Horizontal2") * 180f, Input.GetAxis("Vertical2") * 180f, 0f);

            //Vector3 diff = (-control_pos) - transform.position;

            //diff.Normalize();

            //diff.x = 0;
            //diff.z = 0;

            Charged();

            //Normal();

        }

        void Normal()
        {
            Vector3 diff = GetWorldPositionOnPlane(Input.mousePosition, 0) - transform.position;

            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            mousePos -= new Vector3(0.5f, 0.5f, 0.0f) * 1;

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;


            float rot_z2 = Mathf.Atan2(-diff.y, -diff.x) * -Mathf.Rad2Deg;

            var zLock = Mathf.Clamp(rot_z, -80f, 80f);
            if (zLock < 0) zLock = 360 + zLock;
            var zLock2 = Mathf.Clamp(rot_z2, -80f, 80f);
            if (zLock2 < 0) zLock2 = 360 + zLock2;
            rot_z = zLock;
            rot_z2 = zLock2;

            if (PlayerController.facingright == true)
            {
                if (!once)
                {
                    once = true;
                    once2 = false;
                    //DOTween.Kill(transform);
                }

                if (mousePos.x < 0)
                {
                    /*
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
                    */

                    //transform.localEulerAngles = new Vector3(180, 0, Mathf.Lerp(transform.localEulerAngles.z, 45, Time.deltaTime));
                    //transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(180, 0, 45), 10 * Time.deltaTime);
                    //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 0, -135), 150 * Time.deltaTime);

                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, 150 * Time.deltaTime); // ATE QUE ENFIM FUNCIONOU PORRA

                    //UpdateRotation();
                }
                else
                {
                    //DOTween.Kill(transform);
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
                    //DOTween.Kill(transform);
                }
                if (mousePos.x > 0)
                {
                    /*
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
                    */

                    //transform.localEulerAngles = new Vector3(180, 0, Mathf.Lerp(transform.localEulerAngles.z, 45, Time.deltaTime));
                    //transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(180, 0, 45), 10 * Time.deltaTime);
                    //transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(0, 0, -135), 150 * Time.deltaTime);
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, 150 * Time.deltaTime); // ATE QUE ENFIM FUNCIONOU PORRA
                                                                                                                                       //UpdateRotation();
                }
                else
                {
                    //DOTween.Kill(transform);
                    once4 = false;
                    transform.localEulerAngles = new Vector3(0f, 180f, rot_z2);
                }

            }
        }

        void Charged()
        {
            Vector3 diff = GetWorldPositionOnPlane(Input.mousePosition, 0) - transform.position;

            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            mousePos -= new Vector3(0.5f, 0.5f, 0.0f) * 1;

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;


            float rot_z2 = Mathf.Atan2(-diff.y, -diff.x) * -Mathf.Rad2Deg;

            var zLock = Mathf.Clamp(rot_z, -80f, 80f);
            if (zLock < 0) zLock = 360 + zLock;
            var zLock2 = Mathf.Clamp(rot_z2, -80f, 80f);
            if (zLock2 < 0) zLock2 = 360 + zLock2;
            rot_z = zLock;
            rot_z2 = zLock2;

            if (Input.GetMouseButton(0))
            {
                if (PlayerController.facingright == true)
                {
                    if (!once)
                    {
                        once = true;
                        once2 = false;
                        //DOTween.Kill(transform);
                    }

                    if (mousePos.x < 0)
                    {
                        /*
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
                        */

                        //transform.localEulerAngles = new Vector3(180, 0, Mathf.Lerp(transform.localEulerAngles.z, 45, Time.deltaTime));
                        //transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(180, 0, 45), 10 * Time.deltaTime);
                        //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 0, -135), 150 * Time.deltaTime);

                        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, 150 * Time.deltaTime); // ATE QUE ENFIM FUNCIONOU PORRA

                        //UpdateRotation();
                    }
                    else
                    {
                        //DOTween.Kill(transform);
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
                        //DOTween.Kill(transform);
                    }
                    if (mousePos.x > 0)
                    {
                        /*
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
                        */

                        //transform.localEulerAngles = new Vector3(180, 0, Mathf.Lerp(transform.localEulerAngles.z, 45, Time.deltaTime));
                        //transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(180, 0, 45), 10 * Time.deltaTime);
                        //transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(0, 0, -135), 150 * Time.deltaTime);
                        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, 150 * Time.deltaTime); // ATE QUE ENFIM FUNCIONOU PORRA
                        //UpdateRotation();
                    }
                    else
                    {
                        //DOTween.Kill(transform);
                        once4 = false;
                        transform.localEulerAngles = new Vector3(0f, 180f, rot_z2);
                    }
                }
            }
            else
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, 150 * Time.deltaTime);
            }
        }

        public void UpdateRotation(float z)
        {
            transform.localEulerAngles = new Vector3(180, 0, z);
        }

        public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
            float distance;
            xy.Raycast(ray, out distance);
            return ray.GetPoint(distance);
        }


    }
}
