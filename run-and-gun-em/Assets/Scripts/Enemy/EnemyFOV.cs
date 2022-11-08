using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV: MonoBehaviour
{
    public bool canSeePlayer;

    private int wallMask = 1 << 10;

    private void OnTriggerStay2D(Collider2D collision)
    {     
        if (collision.tag == "Player")
        {
            Vector2 directionToPlayer = (collision.transform.position - transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(transform.position, collision.transform.position);
            
            if (!Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, wallMask))
            {
                canSeePlayer = true;
                Debug.DrawLine(transform.position, collision.transform.position, Color.green);
            }
            else
            {
                canSeePlayer = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canSeePlayer = false;
        }
    }
}
