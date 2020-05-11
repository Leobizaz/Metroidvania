using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class ReactorLightning : MonoBehaviour
{
    public LineRenderer line;
    public Transform lineEnd;
    public int segmentsCount;
    public float cooldown1;
    public float cooldown2;
    public float cooldown3;
    float initialPos1;
    float initialPos2;
    float initialPos3;
    public Light2D light2d;

    public float factor = 0.2f;
    float initialFactor;
    void Start()
    {
        initialFactor = factor;
        initialPos1 = line.GetPosition(2).x;
        initialPos2 = line.GetPosition(1).x;
        initialPos3 = line.GetPosition(3).x;
        factor = initialFactor * 3;
        
    }

    private void OnEnable()
    {
        Invoke("Stabilize", 2);
    }

    // Update is called once per frame
    void Update()
    {



        if (cooldown1 > 0) cooldown1 -= Time.deltaTime;
        if (cooldown2 > 0) cooldown2 -= Time.deltaTime;
        if (cooldown3 > 0) cooldown3 -= Time.deltaTime;

        line.SetPosition(0, this.transform.position);
        line.SetPosition(line.positionCount - 1, lineEnd.position);

        if (cooldown1 <= 0) WeirdLightningMeio();
        if (cooldown2 <= 0) WeirdLightningTop();
        if (cooldown3 <= 0) WeirdLightningBot();
    }

    void WeirdLightningMeio()
    {
        line.SetPosition(2, new Vector3(Random.Range(initialPos1 - factor - 0.1f, initialPos1 + factor +0.1f), line.GetPosition(2).y, 0));
        cooldown1 = Random.Range(0.09f, 0.2f);
        if(light2d != null)
        {
            light2d.pointLightOuterRadius = Random.Range(4.97f, 4.35f);

        }
    }

    void WeirdLightningTop()
    {
        line.SetPosition(1, new Vector3(Random.Range(initialPos2 - factor, initialPos2 + factor), line.GetPosition(1).y, 0));
        cooldown2 = Random.Range(0.05f, 0.3f);
    }

    void WeirdLightningBot()
    {
        line.SetPosition(3, new Vector3(Random.Range(initialPos3 - factor, initialPos3 + factor), line.GetPosition(3).y, 0));
        cooldown3 = Random.Range(0.09f, 0.2f);
    }

    void Stabilize()
    {
        DOVirtual.Float(factor, initialFactor, 3, UpdateFactor);
    }

    void UpdateFactor(float i)
    {
        factor = i;
    }
}
