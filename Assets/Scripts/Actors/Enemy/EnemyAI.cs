using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public bool isWarningPlaced;

    [SerializeField] private GameObject bloodShot;
    [SerializeField] private GameObject bloodDeath;
    [SerializeField] private GameObject healthKit;
    [SerializeField] private GameObject EnemyWarning;

    private EnemyWeapon weapon;
    private EnemySight enemySight;
    private Transform playerTransform;
    private new Rigidbody2D rigidbody;
    private new Renderer renderer;
    private Animator animator;

    private Vector2 lastKnownPosition;
    private IEnumerator coroutine;

    private readonly float turnSpeed = 30f;
    private readonly float warningDelay = 0.7f;
    private readonly int healthKitDropChance = 20;
    private readonly int moveSpeed = 8;
    private readonly int searchAngle = 30;
    private readonly int searchTolerance = 3;
    private float warningTimer = 0;
    private int health = 2;
    private bool isMovingToPlayer;
    private bool passiveSearch;


    private void Awake()
    {
        enemySight = GetComponentInChildren<EnemySight>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weapon = GetComponent<EnemyWeapon>();
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        passiveSearch = true;
        IEnumerator coroutine = PassiveSearchRoutine();
        StartCoroutine(coroutine);
    }

    /*
     * Kollar om kan se spelare
     * Sätter ut varnig
     * Skjuter
     */
    private void Update()
    {
        if (enemySight.canSeePlayer)
        {
            if(!renderer.isVisible && !isWarningPlaced)
            {
                PlaceWarning();
            }

            LookAt(playerTransform.position);
            if (Time.time - warningTimer > warningDelay)
            {
                weapon.Shoot();
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (isMovingToPlayer)
        {
            MoveTo(lastKnownPosition);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Damage(1);
        }
    }

    /*
     * När enemy tappar spelaren startar den funktionerna för att leta spelaren
     */
    public void OnLostPlayer(Vector2 lastPosition)
    {
        animator.SetBool("isShooting", false);
        lastKnownPosition = lastPosition;
        isMovingToPlayer = true;
    }

    /*
     * Vänder Enemy mot target om Enemy inte ser spelaren
     * Kan kallas 1 gång och körs över flera frames
     */
    public IEnumerator LookAtRoutine(Vector2 target)
    {
        passiveSearch = false;
        yield return null;
        float _turnSpeed = turnSpeed * 0.1f;
        Vector2 desiredDirection = target - (Vector2)transform.position;
        float desiredAngle = Mathf.Atan2(desiredDirection.y, desiredDirection.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, desiredAngle)) > searchTolerance && !enemySight.canSeePlayer)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, desiredAngle), _turnSpeed * Time.deltaTime);
            yield return null;
        }
    }

    /*
     * Fienden vänder sig om då den inte har sett spelaren än
     */
    private IEnumerator PassiveSearchRoutine()
    {
        float angle0 = transform.eulerAngles.z + searchAngle + Random.Range(0, 10);
        float angle1 = transform.eulerAngles.z - searchAngle + Random.Range(0, 10);
        float desiredAngle = angle0;
        float _turnSpeed = turnSpeed * 0.05f;

        while (passiveSearch)
        {
            while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, desiredAngle)) > searchTolerance && passiveSearch)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, desiredAngle), _turnSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(0.6f, 1.2f));
            desiredAngle = desiredAngle == angle0 ? angle1 : angle0;
        }
    }

    /*
     * Vänder sig mot target
     * Måste kallas flera gånger
     * Enemy ser nu spelaren
     */
    private void LookAt(Vector2 target)
    {
        passiveSearch = false;

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
        {
            Die();
        }
        else
        {
            AudioManager.instance.Play("EnemyHurt");
            Instantiate(bloodShot, transform.position, Quaternion.identity);
            
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
        Instantiate(bloodDeath, transform.position, Quaternion.identity);
        

        GameManager.instance.OnEnemyKilled();

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
            isMovingToPlayer = false;
            animator.SetBool("isMoving", false);
        }
    }

    /*
     * Sätter ut en varning
     * Kallas då spelaren inte ser fienden men fienden ser spelaren
     */
    private void PlaceWarning()
    {
        Instantiate(EnemyWarning, transform.position, Quaternion.identity, transform);
        isWarningPlaced = true;

        if (passiveSearch)
        {
            warningTimer = Time.time;
        }
    }
}