using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private EnemySight sight;
    private EnemyAI AI;

    private readonly float turnSpeed = 30f;
    private readonly double positionTolerance = 0.1;
    private readonly int moveSpeed = 8;
    private readonly int searchTolerance = 3;
    private readonly int searchAngle = 30;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        AI = GetComponent<EnemyAI>();
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
            AI.isMovingToPlayer = false;
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
        AI.passiveSearch = false;

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
        AI.passiveSearch = false;
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

    /*
     * Fienden vänder sig om då den inte har sett spelaren än
     */
    public IEnumerator PassiveSearchRoutine()
    {
        float angle0 = transform.eulerAngles.z + searchAngle + Random.Range(0, 10);
        float angle1 = transform.eulerAngles.z - searchAngle + Random.Range(0, 10);
        float desiredAngle = angle0;
        float _turnSpeed = turnSpeed * 0.05f;

        while (AI.passiveSearch)
        {
            while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, desiredAngle)) > searchTolerance && AI.passiveSearch)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, desiredAngle), _turnSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(0.6f, 1.2f));
            desiredAngle = desiredAngle == angle0 ? angle1 : angle0;
        }
    }
}
