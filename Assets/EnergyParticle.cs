using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyParticle : MonoBehaviour
{
    ParticleSystem pSys;
    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.CollisionModule collision;
    ParticleSystem.ExternalForcesModule pSysModule;
    ParticleSystem.TriggerModule pSysTriggerModule;
    int count;
    private void Start()
    {
        pSys = GetComponent<ParticleSystem>();
        pSysModule = pSys.externalForces;
        pSysTriggerModule = pSys.trigger;
        collision = pSys.collision;
        emissionModule = pSys.emission;
        pSys.trigger.SetCollider(0, GameObject.Find("EnergyCollector").GetComponent<Collider>());
        pSysModule.enabled = false;
        pSysTriggerModule.enabled = false;
        Invoke("ActivateField", 2);
    }

    void ActivateField()
    {
        pSysModule.enabled = true;
        collision.enabled = false;
        pSysTriggerModule.enabled = true;
    }

    private void OnParticleTrigger()
    {
        count++;
        if (count < emissionModule.GetBurst(0).count.constant)
        {
            EnergyCollector.current.ChargeCollector();
        }
    }
}
