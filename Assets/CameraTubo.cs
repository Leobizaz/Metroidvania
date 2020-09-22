using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using BansheeGz;
using BansheeGz.BGSpline.Curve;
using BansheeGz.BGSpline.Components;

public class CameraTubo : MonoBehaviour
{
    public CinemachineDollyCart cart;
    bool moving;
    public BGCurve curva;
    public BGCcMath spline;
    float distance;
    public GameObject cameraPLAYER;

    private void Start()
    {

    }

    private void Update()
    {
        if (moving)
        {
            distance += 14 * Time.deltaTime;

            Vector3 tangent;
            cart.gameObject.transform.position = spline.CalcPositionAndTangentByDistance(distance, out tangent);
            //cart.gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(-tangent.y, -tangent.x) * Mathf.Rad2Deg, Vector3.forward);

            if (distance >=255)
            {
                moving = false;
                cameraPLAYER.SetActive(true);
                //cart.m_Speed = 0;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        cart.gameObject.transform.position = curva.Points[0].PositionWorld;
        distance = 0;
        //cart.m_Position = 0;
        moving = true;
        //cart.m_Speed = 2;

    }
}
