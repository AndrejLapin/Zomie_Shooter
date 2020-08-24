using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] bool semiAuto = true;
    [SerializeField] float range = 100f;
    [SerializeField] float gunDamage = 35f;
    [SerializeField] int ammoPerShot = 1;
    [SerializeField] float shotDelay = 0.5f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] TextMeshProUGUI ammoText;

    bool ableToShoot = true;

    private void OnEnable()
    {
        StartCoroutine(ShotDelay());
    }

    void Update()
    {
        DisplayAmmo();
        if (semiAuto)
        {
            if (Input.GetMouseButtonDown(0) && ammoSlot.GetCurrentAmmo(ammoType) > 0 && ableToShoot)
            {
                Shoot();
                StartCoroutine(ShotDelay());
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && ammoSlot.GetCurrentAmmo(ammoType) > 0 && ableToShoot)
            {
                Shoot();
                StartCoroutine(ShotDelay());
            }
        }

    }

    private void DisplayAmmo()
    {
        ammoText.text = ammoSlot.GetCurrentAmmo(ammoType).ToString();
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.ReduceCurrentAmmo(ammoPerShot, ammoType);
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            //print("I hit this: " + hit.transform.name); // DEBUG LINE
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(gunDamage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        var newHitEffect = Instantiate(hitEffect, hit.point, Quaternion.identity);
        Destroy(newHitEffect, 0.1f);
    }

    IEnumerator ShotDelay()
    {
        ableToShoot = false;
        yield return new WaitForSeconds(shotDelay);
        ableToShoot = true;
    }
}
