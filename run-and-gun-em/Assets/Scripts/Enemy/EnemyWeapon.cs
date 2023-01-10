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

    private readonly float bulletSpeed = 25;
    private readonly float bulletDelay = 0.4f;
    private readonly float bulletSpreadMultiplier = 2;
    private float lastShot = 0;
    private int ammoCount = 30;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Shoot()
    {
        if (Time.time - lastShot > bulletDelay && ammoCount > 0)
        {
            animator.SetBool("isShooting", true);
            lastShot = Time.time;
            ammoCount -= 1;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

            rigidbody.velocity = firePoint.right * bulletSpeed + UnityEngine.Random.Range(0, bulletSpreadMultiplier) * (UnityEngine.Random.Range(0, 2) * 2 - 1) * firePoint.right;
        }
        else if (ammoCount == 0)
        {
            animator.SetBool("isShooting", false);
            Reload();
        }
    }

    public void Reload()
    {
        ammoCount = 30;
    }
}
