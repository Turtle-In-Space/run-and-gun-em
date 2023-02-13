using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyWeapon weapon;
    [SerializeField] private GameObject bloodShot;
    [SerializeField] private GameObject bloodDeath;
    private EnemyFOV enemyFOV;
    private GameObject player;

    private readonly float turnSpeed = 0.2f;
    private int health = 2;


    void Awake()
    {        
        enemyFOV = GetComponentInChildren<EnemyFOV>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (enemyFOV.canSeePlayer)
        {
            Vector2 lookDirection = player.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle + 5), turnSpeed);

            weapon.Shoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Damage(1);
        }
    }

    private void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
        else
        {
            GameObject blood = Instantiate(bloodShot, transform.position, Quaternion.identity);
            Destroy(blood, 3f);
        }
    }

    private void Die()
    {
        GameObject blood = Instantiate(bloodDeath, transform.position, Quaternion.identity);
        Destroy(blood, 3f);

        HUD.instace.SetScore(100);
        Game_Manager.instance.EnemyKilled();
        Destroy(gameObject);
    }
}
