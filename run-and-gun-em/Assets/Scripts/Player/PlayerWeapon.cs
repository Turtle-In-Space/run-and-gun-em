using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerUI playerUI;

    [SerializeField] private int bulletSpread;
    private int bulletSpeed = 50;
    private float bulletDelay = 0.2f;
    private float lastShot = 0;
    private int ammoCount = 30;


    private void Start()
    {
        animator.GetBehaviour<CallReload>().playerWeapon = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && ammoCount != 30)
        {
            animator.SetTrigger("Reload");
            ammoCount = playerUI.SetAmmoCount(0);            
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && ammoCount > 0)
        {
            animator.SetBool("isFiring", true);

            if (Input.GetButton("Fire1") && Time.time - lastShot > bulletDelay)
            {
                lastShot = Time.time;
                Shoot();
                ammoCount = playerUI.SetAmmoCount(ammoCount - 1);
            }
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.velocity = firePoint.right * bulletSpeed;
    }

    public void OnReloadFinished()
    {
        ammoCount = playerUI.SetAmmoCount(30);
    }
}
