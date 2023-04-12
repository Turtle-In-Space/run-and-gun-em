using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private EnemySight sight;
    private EnemyAI enemyAI;

    private readonly float turnSpeed = 30f;
    private readonly double positionTolerance = 0.1;
    private readonly int moveSpeed = 8;
    private readonly int searchTolerance = 3;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
        sight = GetComponentInChildren<EnemySight>();

    }

    /*
     * Flyttar enemy till position
     * Stannar när nästan där
     */
    public void MoveTo(Vector2 position)
    {
        LookAt(position);

        animator.SetBool("isMoving", true);
        Vector2 direction = position - (Vector2)transform.position;
        rigidbody.MovePosition((Vector2)transform.position + (moveSpeed * Time.deltaTime * direction.normalized));

        if (Vector2.Distance(transform.position, position) < positionTolerance)
        {
            enemyAI.isMovingToPlayer = false;
            animator.SetBool("isMoving", false);
        }
    }

    /*
    * Vänder sig mot target
    * Måste kallas flera gånger
    * Enemy ser nu spelaren
    */
    public void LookAt(Vector2 target)
    {
        enemyAI.passiveSearch = false;

        Vector2 lookDirection = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed * Time.deltaTime);
    }

    /*
    * Vänder Enemy mot target om Enemy inte ser spelaren
    * Kan kallas 1 gång och körs över flera frames
    */
    public IEnumerator LookAtRoutine(Vector2 target)
    {
        enemyAI.passiveSearch = false;
        yield return null;
        float _turnSpeed = turnSpeed * 0.1f;
        Vector2 desiredDirection = target - (Vector2)transform.position;
        float desiredAngle = Mathf.Atan2(desiredDirection.y, desiredDirection.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, desiredAngle)) > searchTolerance && !sight.canSeePlayer)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, desiredAngle), _turnSpeed * Time.deltaTime);
            yield return null;
        }
    }   
}
