using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class Boss_C2 : MonoBehaviour
{
    public Color32 opacidadeDesejada;
    public Color32 colorOld;
    public Light2D[] luzes_ponto;
    public SpriteRenderer[] luzes_material;
    public Transform[] projectile_points;
    public GameObject projectilePrefab;
    bool block = true;
    public Animator bossAnimator;
    public AudioSource audio;
    public AudioClip[] sonsRugido;

    private float timeToLightUP = 6;

    private Light2D currentLuz;

    private void Start()
    {
        foreach(Light2D luz in luzes_ponto)
        {
            luz.intensity = 0;
        }
        foreach(SpriteRenderer luzMaterial in luzes_material)
        {
            luzMaterial.color = colorOld;
        }

        gameObject.SetActive(false);
        block = false;
        audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (!block)
        Initialize();
    }

    public void Initialize()
    {
        DOVirtual.Float(0, 0.8f, timeToLightUP, UpdateLight);

        foreach(SpriteRenderer luzMaterial in luzes_material)
        {
            luzMaterial.DOColor(opacidadeDesejada, timeToLightUP);
        }
        StartCoroutine(SequencialShooting());
    }

    void UpdateLight(float v)
    {
        foreach(Light2D luz in luzes_ponto)
        {
            luz.intensity = v;
        }
    }

    IEnumerator SequencialShooting()
    {
        while (true)
        {
            bossAnimator.Play("boss_roar");
            audio.clip = sonsRugido[Random.Range(0, sonsRugido.Length)];
            audio.Play();
            spawnProjectile(Random.Range(0, projectile_points.Length));
            yield return new WaitForSeconds(0.3f);
            spawnProjectile(Random.Range(0, projectile_points.Length));
            yield return new WaitForSeconds(0.3f);
            spawnProjectile(Random.Range(0, projectile_points.Length));
            yield return new WaitForSeconds(0.3f);
            spawnProjectile(Random.Range(0, projectile_points.Length));
            yield return new WaitForSeconds(10);
        }
    }


    public void spawnProjectile(int index)
    {
        GameObject projectile = Instantiate(projectilePrefab, projectile_points[index].position, Quaternion.identity);
        projectile.transform.position = new Vector3(projectile_points[index].position.x, projectile_points[index].position.y, 0);

    }

    public void GetHit()
    {

    }

}
