using UnityEngine;

public class EnemySight: MonoBehaviour
{
    [HideInInspector]
    public bool canSeePlayer;

    private EnemyAI AI;

    private readonly int wallMask = 1 << 10;
    private readonly int doorMask = 1 << 13;


    private void Awake()
    {
        AI = GetComponentInParent<EnemyAI>();
    }

    /*
     * Kollar om Enemy kan se spelare
     * Om enemy tappar spelare -> leta
     */
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {           
            Vector2 directionToPlayer = (collision.transform.position - transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(transform.position, collision.transform.position);

            //Kollar om det finns en vägg/dörr i vägen
            if (!Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, wallMask))
            {
                if (!Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, doorMask))
                {
                    canSeePlayer = true;
                }
            }
            else
            {
                if (canSeePlayer)
                {
                    AI.OnLostPlayer(collision.transform.position);
                }

                canSeePlayer = false;
            }
        }
    }

    /*
     * Om enemy ser spelaren sen lämnare spelaren FOV
     * Starta funktioner för att leta spelare
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canSeePlayer)
            {
                AI.OnLostPlayer(collision.transform.position);
                canSeePlayer = false;
            }            
        }
    }
}
