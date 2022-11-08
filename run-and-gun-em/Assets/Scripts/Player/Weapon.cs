using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private Transform firePoint;
    private Text bulletText;
    private Animator animator;

    [SerializeField] private float bulletSpreadMultiplier = 1;
    [SerializeField] private float bulletSpeed = 20f;

    private float bulletDelay = 0.2f;
    private float lastShot = 0;
    private int ammoCount = 30;
    private int newAmmo;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bulletText = GameObject.Find("AmmoCount").GetComponent<Text>();
        firePoint = transform.GetChild(0).GetComponent<Transform>();
    }

    private void Update()
    {       
        if (Input.GetKeyDown(KeyCode.R) && ammoCount != 30)
        {
            animator.SetTrigger("Reload");
            newAmmo = 0;
            SetAmmoCount();
            newAmmo = 30;
            Invoke("SetAmmoCount", 1);
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
                newAmmo = ammoCount - 1;
                SetAmmoCount();
            }
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }

    private void SetAmmoCount()
    {
        ammoCount = newAmmo;
        bulletText.text = ammoCount.ToString() + "/30";
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.velocity = firePoint.right * bulletSpeed + Random.Range(0, bulletSpreadMultiplier) * (Random.Range(0, 2) * 2 - 1) * firePoint.right;
    }
}
