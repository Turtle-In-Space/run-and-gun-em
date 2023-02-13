using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    private Animator animator;

    private readonly float bulletForce = 30;
    private readonly float bulletDelay = 0.3f;
    private readonly float spreadMultiplier = 0.2f;
    private float lastShot = 0;
    private int ammoCount = 30;
    

    private void Awake()
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

            Vector2 bulletDirection = new Vector2(firePoint.right.x + Random.Range(-spreadMultiplier, spreadMultiplier), firePoint.right.y + Random.Range(-spreadMultiplier, spreadMultiplier)).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(bulletDirection * bulletForce, ForceMode2D.Impulse);
        }
        else if (ammoCount == 0)
        {
            animator.SetBool("isShooting", false);
            Reload();
        }
    }

    private void Reload()
    {
        ammoCount = 30;
    }
}
