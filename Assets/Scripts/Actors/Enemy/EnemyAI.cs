using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public bool isWarningPlaced;
    public bool isMovingToPlayer;
    public bool passiveSearch;

    [SerializeField] private GameObject bloodShot;
    [SerializeField] private GameObject bloodDeath;
    [SerializeField] private GameObject healthKit;
    [SerializeField] private GameObject EnemyWarning;

    private EnemyWeapon weapon;
    private EnemySight sight;
    private Transform playerTransform;
    private new Renderer renderer;
    private Animator animator;
    private EnemyMovement movement;

    private Vector2 lastKnownPosition;
    private IEnumerator coroutine;

    private readonly float warningDelay = 0.7f;
    private readonly int healthKitDropChance = 20;
    
    private float warningTimer = 0;
    private int health = 2;
    

    private void Awake()
    {
        sight = GetComponentInChildren<EnemySight>();
        animator = GetComponent<Animator>();
        weapon = GetComponent<EnemyWeapon>();
        renderer = GetComponent<Renderer>();
        movement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        passiveSearch = true;
        IEnumerator coroutine = movement.PassiveSearchRoutine();
        StartCoroutine(coroutine);
    }

    /*
     * Kollar om kan se spelare
     * Sätter ut varnig
     * Skjuter
     */
    private void Update()
    {
        if (sight.canSeePlayer)
        {
            if(!renderer.isVisible && !isWarningPlaced)
            {
                PlaceWarning();
            }

            movement.LookAt(playerTransform.position);
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
            movement.MoveTo(lastKnownPosition);
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
            
            coroutine = movement.LookAtRoutine(playerTransform.position);
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