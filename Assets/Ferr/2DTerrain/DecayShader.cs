using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayShader : MonoBehaviour
{

    Material mat;
    public GameObject Effector;
    public SphereCollider effectorCollider;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().materials[1];
    }

    private void Update()
    {
        mat.SetVector("_EffectPosition", Effector.transform.position - this.transform.position);
        mat.SetFloat("_EffectRadius", effectorCollider.radius * Effector.transform.localScale.magnitude);
    }
}
