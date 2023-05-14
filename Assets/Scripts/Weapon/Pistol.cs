﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{

    public ParticleSystem muzzleFlash;

    Animator anim;

    AudioSource pistolAS;
    public AudioClip shootAC;
    public AudioClip emptyFire;


    bool isReloading;

    RaycastHit hit;

    
    public int currentAmmo=12; //suan ki mermi sayısı
    public int maxAmmo=12; // max mermi sayısı
    public int carriedAmmo=60; //cebimdeki mermi sayısı tasiyabildigim

    public GameObject metalBulletHole;
    public AudioClip shootMetalAC;


    [SerializeField]
    float rateOffFire;

    float nextfire=0;

    [SerializeField]
    float weaponRange;

    public float damage = 20f;


    public Transform shootPoint; //atis noktasi

    

    public Text currentAmmoText;
    public Text carriedAmmoText;

    public GameObject bloodEffect;

    void Start()
    {
        UpdateAmmoUI();
        anim = GetComponent<Animator>();
        pistolAS = GetComponent<AudioSource>();
        muzzleFlash.Stop();
       
    }

    
    void Update()
    {
        if(Input.GetButton("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
        else if(Input.GetButton("Fire1") && currentAmmo <= 0 && !isReloading)
        {
            EmptyFire();
        }

        else if(Input.GetKeyDown(KeyCode.R) && currentAmmo <= maxAmmo && !isReloading)
        {
            isReloading = true;
            Reload();
        }
    }

    void Shoot()
    {
        if(Time.time > nextfire)
        {
            nextfire = Time.time + rateOffFire;
            anim.SetTrigger("Shoot");
            currentAmmo --;

            ShootRay();

            UpdateAmmoUI();
        }
    }

    void ShootRay()
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange))
        {
            if (hit.transform.tag == "Enemy")
            {
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                Instantiate(bloodEffect, hit.point, transform.rotation);
                enemy.ReduceHealth(damage);
            }

            else if (hit.transform.tag == "Metal")
            {
                pistolAS.PlayOneShot(shootMetalAC);
                Instantiate(metalBulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

            }
        
        else
        {
            Debug.Log("Something else");
        }

        }
    }

    void Reload()
    {
        if (carriedAmmo <= 0) return;
        anim.SetTrigger("Reload");
        StartCoroutine(ReloadCountDown(2f));
       
    }

    public void UpdateAmmoUI()
    {
        currentAmmoText.text = currentAmmo.ToString();
        carriedAmmoText.text = carriedAmmo.ToString();

    }

    void EmptyFire()
    {
        if (Time.time > nextfire)
        {
            nextfire = Time.time + rateOffFire;
            pistolAS.PlayOneShot(emptyFire);
            anim.SetTrigger("Empty");

        }
    }

    IEnumerator pistolEffect()
    {
        muzzleFlash.Play();
        pistolAS.PlayOneShot(shootAC);
        yield return new WaitForEndOfFrame();
        muzzleFlash.Stop();
    }

    IEnumerator ReloadCountDown(float timer)
    {
       while(timer > 0f)
        {
            
            timer -= Time.deltaTime;
            yield return null;
        }

       if(timer <= 0)
        {
            isReloading = false;
            int bulletNeeded=maxAmmo-currentAmmo;
            int bulletsToDeduct = (carriedAmmo >= bulletNeeded) ? bulletNeeded : carriedAmmo;
            carriedAmmo -= bulletsToDeduct;
            currentAmmo += bulletsToDeduct;
            UpdateAmmoUI();



        }
    }

}
