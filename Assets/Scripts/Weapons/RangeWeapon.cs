using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : AbstractItem
{
    public int damage = 50;
    public float range = 50f;
    public int pellets = 8; // Количество дробинок
    public float spread = 5f; // Разброс выстрела
    public GameObject muzzleFlashEffect;
    public Transform shootPoint; // Точка, из которой стреляем
    public AudioClip shootSound;
    public AudioSource source;

    private bool canShoot = true;
    public float fireRate = 1f; // Время между выстрелами

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
        }
    }

    public void UpdatePosition()
    {
        transform.localPosition = new Vector3(-0.3f, 0.5f, 0f);
    }

    private void Shoot()
    {
        canShoot = false;

        if (muzzleFlashEffect != null)
        {
            Instantiate(muzzleFlashEffect, shootPoint.position, shootPoint.rotation);
        }

        //AudioSystem.Instance.PlaySoundOnce(shootSound, transform);
        source.PlayOneShot(shootSound);
        //Debug.Log("Shooting...");
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, range))
        {
            Debug.Log("Попал в " + hit.transform.name);
            hit.transform.GetComponent<Zombie>()?.ReceiveDamage(damage);
        }
        // for (int i = 0; i < pellets; i++)
        // {
        //     Vector3 direction = shootPoint.forward +
        //                         new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0);
        //     
        // }

        Invoke(nameof(ResetShot), 1f / fireRate); // Перезарядка дробовика
    }

    private void ResetShot()
    {
        canShoot = true;
    }
}