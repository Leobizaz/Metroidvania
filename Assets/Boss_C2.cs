using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject tubo;
    public AudioSource audio;
    public AudioSource bossEnd;
    public ParticleSystem explodeBig;
    public ParticleSystem explodesmall;
    public AudioSource music;
    public Image barFill;
    public GameObject hpBar;
    bool barActive;
    private float timeToLightUP = 6;
    public float hp = 100;
    bool dead;
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
        if (!dead)
        {
            DOVirtual.Float(0, 0.8f, timeToLightUP, UpdateLight);

            foreach (SpriteRenderer luzMaterial in luzes_material)
            {
                luzMaterial.DOColor(opacidadeDesejada, timeToLightUP);
            }
            bossAnimator.Play("boss_roar");
            Invoke("ShowHPBar", 2);
            Invoke("StartFight", 6);
        }
    }

    void ShowHPBar()
    {
        hpBar.SetActive(true);
        Invoke("ActivateBar", 2);
    }
    void ActivateBar()
    {
        barActive = true;
    }

    private void Update()
    {
        if(barActive)
            barFill.fillAmount = Mathf.InverseLerp(0, 100, hp);

        if(hp <= 0 && !dead)
        {
            dead = true;
            explodeBig.Play();
            StopAllCoroutines();
            CancelInvoke("StartFight");
            hpBar.SetActive(false);
            bossAnimator.Play("boss_morte");
            music.Stop();
            bossEnd.Play();
            Invoke("ActivateTubo", 2);
        }

    }

    void ActivateTubo()
    {
        tubo.SetActive(true);
    }


    void StartFight()
    {
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
            if (!dead)
            {
                bossAnimator.Play("boss_roar");
                spawnProjectile(Random.Range(0, projectile_points.Length));
                yield return new WaitForSeconds(0.3f);
                spawnProjectile(Random.Range(0, projectile_points.Length));
                yield return new WaitForSeconds(0.3f);
                spawnProjectile(Random.Range(0, projectile_points.Length));
                yield return new WaitForSeconds(0.3f);
                spawnProjectile(Random.Range(0, projectile_points.Length));
            }
            yield return new WaitForSeconds(10);
        }
    }


    public void spawnProjectile(int index)
    {
        GameObject projectile = Instantiate(projectilePrefab, projectile_points[index].position, Quaternion.identity);
        projectile.transform.position = new Vector3(projectile_points[index].position.x, projectile_points[index].position.y, 0);

    }

    public void GetHitBig()
    {
        if (hp > 40)
        {
            CancelInvoke("StartFight");
            StopAllCoroutines();
            bossAnimator.Play("boss_hit");
            hp -= 40;
            Invoke("StartFight", 2);
        }
        else
        {
            CancelInvoke("StartFight");
            StopAllCoroutines();
            bossAnimator.Play("boss_hit");
            hp = hp/2;
            Invoke("StartFight", 2);
        }
    }

    public void GetHit()
    {
        if (hp > 0)
        {
            explodesmall.Play();
            CancelInvoke("StartFight");
            StopAllCoroutines();
            bossAnimator.Play("boss_hit");
            hp -= 20;
            Invoke("StartFight", 6);
        }
    }

}
