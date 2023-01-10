using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private EnemyWeapon weapon;
    private EnemyFOV enemyFOV;
    private GameObject player;

    private float turnSpeed = 0.2f;   


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyFOV = GetComponentInChildren<EnemyFOV>();
        weapon = GetComponent<EnemyWeapon>();
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
            Destroy(gameObject);
        }
    }  
}
