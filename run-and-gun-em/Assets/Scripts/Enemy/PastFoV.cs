using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastFov : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    [SerializeField] private float FOV = 60f;

    [SerializeField] public bool canSeePlayer = false;


    public Vector2 playerPos;

    private int playerMask = 1<<6;
    private int wallMask = 1<<10;


    public IEnumerator VisionRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            VisionCheck();
        }                 
    }

    private void VisionCheck()
    {
        Collider2D inRange = Physics2D.OverlapCircle(transform.position, radius, playerMask);
        if (inRange != null)
        {
            playerPos = inRange.transform.position;
            Vector2 directionToPlayer = (playerPos - (Vector2)transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(transform.position, playerPos);
            float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);

            if (angleToPlayer <= FOV / 2)
            {
                if (!Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, wallMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        float angle = radius * Mathf.Tan(FOV / 2 * Mathf.Deg2Rad);

        Vector3 lookPoint01 = new Vector3(radius, angle).normalized;
        Vector3 lookPoint02 = new Vector3(lookPoint01.x, -lookPoint01.y);

        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawRay(transform.position, lookPoint01.Rotate(transform.rotation.eulerAngles.z) * radius);
        Gizmos.DrawRay(transform.position, lookPoint02.Rotate(transform.rotation.eulerAngles.z) * radius);
    }
}