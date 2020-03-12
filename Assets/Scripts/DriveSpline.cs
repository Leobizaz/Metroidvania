using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;

[ExecuteInEditMode]
public class DriveSpline : MonoBehaviour
{

    public BGCcMath spline;
    public float distance;
    public bool move;


    void Update()
    {
        //increase distance
        if(move)
            distance += 7 * Time.deltaTime;

        //calculate position and tangent
        Vector3 tangent;
        transform.position = spline.CalcPositionAndTangentByDistance(distance, out tangent);
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(-tangent.y, -tangent.x) * Mathf.Rad2Deg, Vector3.forward);
    }
}
