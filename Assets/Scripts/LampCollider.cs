using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.Universal
{
    public class LampCollider : MonoBehaviour
    {
        CircleCollider2D circleCollider;
        Light2D light;

        private void Start()
        {
            circleCollider = GetComponent<CircleCollider2D>();
            light = GetComponent<Light2D>();

            circleCollider.radius = light.pointLightOuterRadius;
        }
    }
}
