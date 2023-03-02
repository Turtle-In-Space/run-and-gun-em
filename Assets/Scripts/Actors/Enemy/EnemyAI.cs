using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject bloodShot;
    [SerializeField] private GameObject bloodDeath;
    [SerializeField] private GameObject healthKit;

    private EnemyWeapon weapon;
    private EnemyFOV enemyFOV;
    private Transform playerTransform;
    private new Rigidbody2D rigidbody;
    private Animator animator;

    private Vector2 lastKnownPosition;
    private IEnumerator coroutine;

    private readonly int healthKitDropChance = 10;
    private readonly int moveSpeed = 8;
    private readonly float turnSpeed = 30;
    private int health = 2;
    private bool lookingForPlayer;


    private void Awake()
    {
        enemyFOV = GetComponentInChildren<EnemyFOV>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weapon = GetComponent<EnemyWeapon>();
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(enemyFOV.canSeePlayer)
        {
            LookAt(playerTransform.position);
            weapon.Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (lookingForPlayer)
        {
            MoveTo(lastKnownPosition);
        }
    }  

    /*
     * När enemy tappar spelaren startar den funktionerna för att leta spelaren
     */
    public void OnLostPlayer(Vector2 lastPosition)
    {
        animator.SetBool("isShooting", false);
        lastKnownPosition = lastPosition;
        lookingForPlayer = true;
    }

    /*
     * Vänder Enemy mot target om Enemy inte ser spelaren
     * Kan kallas 1 gång och körs över flera frames
     */
    public IEnumerator LookAtRoutine(Vector2 target)
    {
        Vector2 lookDirection = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        float _turnspeed = turnSpeed * 0.1f;

        while (Mathf.Round(transform.eulerAngles.z) != Mathf.Round(angle) && !enemyFOV.canSeePlayer)
        {
            lookDirection = target - (Vector2)transform.position;
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), _turnspeed * Time.deltaTime);
            yield return null;

        }
    }

    /*
     * Vänder sig mot target
     * Måste kallas flera gånger
     */
    private void LookAt(Vector2 target)
    {
        Vector2 lookDirection = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed * Time.deltaTime);
    }    

    /*
     * Sänker fiendens hp med amount
     * Spelar ljud och bild effekt 
     * Försöker vända Enemy
     */
    private void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
        else
        {
            AudioManager.instance.Play("EnemyHurt");
            GameObject blood = Instantiate(bloodShot, transform.position, Quaternion.identity);
            Destroy(blood, 3f);

            coroutine = LookAtRoutine(playerTransform.position);
            StartCoroutine(coroutine);         
        }
    }

    /*
     * Spelar ljud och bild effekter
     * Chans att droppa healthkit
     * Tar bort enemy
     */
    private void Die()
    {
        AudioManager.instance.Play("EnemyDeath");
        GameObject blood = Instantiate(bloodDeath, transform.position, Quaternion.identity);
        Destroy(blood, 3f);

        Game_Manager.instance.OnEnemyKilled();

        int value = Random.Range(0, 100);
        if (value < healthKitDropChance)
        {
            Instantiate(healthKit, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    /*
     * Flyttar enemy till position
     * Stannar när nästan där
     */
    private void MoveTo(Vector2 position)
    {
        LookAt(position);

        animator.SetBool("isMoving", true);
        Vector2 direction = position - (Vector2)transform.position;
        rigidbody.MovePosition((Vector2)transform.position + (moveSpeed * Time.deltaTime * direction.normalized));


        if (Vector2.Distance(transform.position, position) < 0.1)
        {
            lookingForPlayer = false;
            animator.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Damage(1);
        }
    }
}
