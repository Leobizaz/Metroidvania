
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
        public Camera cam;
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

            //Charged();

            //Normal();

        }

        private void FixedUpdate()
        {
            Charged();
        }

        void Normal()
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Vector3 diff = point - transform.position;

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
            //Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //diff.Normalize();

            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(mousePos);

            //Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Vector3 point = getCursorWorldPosition();
            Vector3 diff = point - transform.position;

            //Vector3 diff = GetWorldPositionOnPlane(Input.mousePosition, 0) - transform.position;
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

        Vector3 getCursorWorldPosition()
        {
            //finding point in world in cursor position
            Plane groundPlane = new Plane(Vector3.forward, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            float distance;
            //simply initializing vector3 point, nothing else, this vector zero does nothing
            Vector3 point = Vector3.zero;
            if (groundPlane.Raycast(ray, out distance))
            {
                point = ray.GetPoint(distance);
            }

            return point;
        }


    }
}