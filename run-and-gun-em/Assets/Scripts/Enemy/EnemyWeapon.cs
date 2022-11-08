using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    private Animator animator;

    private float bulletSpeed = 30;
    private float bulletSpreadMultiplier = 2;
    private float bulletDelay = 0.2f;
    private float lastShot = 0;
    private int ammoCount = 30;
    private int newAmmo;


    private void Attack()
    {
        //Turn to player
        //While see player
        //SHOOT

    }


    public void Shoot()
    {
        if (Time.time - lastShot > bulletDelay && ammoCount > 0)
        {
            lastShot = Time.time;
            newAmmo = ammoCount - 1;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

            rigidbody.velocity = firePoint.up * bulletSpeed + UnityEngine.Random.Range(0, bulletSpreadMultiplier) * (UnityEngine.Random.Range(0, 2) * 2 - 1) * firePoint.right;
        }
    }
}
