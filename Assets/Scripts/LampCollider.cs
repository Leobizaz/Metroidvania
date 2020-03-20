using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.Universal
{
    public class LampCollider : MonoBehaviour
    {
        CircleCollider2D circleCollider;
        Light2D lightt;

        private void Start()
        {
            circleCollider = GetComponent<CircleCollider2D>();
            lightt = GetComponent<Light2D>();

            circleCollider.radius = lightt.pointLightOuterRadius;
        }
    }
}
