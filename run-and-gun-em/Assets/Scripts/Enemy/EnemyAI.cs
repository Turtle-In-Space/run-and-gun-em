using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private EnemyFOV enemyFOV;

    private GameObject player;

    [SerializeField] private float turnSpeed = 0.2f;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyFOV = GetComponentInChildren<EnemyFOV>();
    }

    void FixedUpdate()
    {
        if (enemyFOV.canSeePlayer)
        {
            Vector2 lookDirection = player.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed);
        }
    }


    private void Shoot()
    {
        //Spawn bullet
        //Add force
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }    
}
