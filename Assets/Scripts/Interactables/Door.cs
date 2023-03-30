using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [HideInInspector]
    public bool isDoorExplodable = false;

    [SerializeField] private GameObject explosionPrefab;

    private GameObject EKey;

    private readonly int enemyLayerMask = 1 << 8;


    private void Awake()
    {
        EKey = transform.parent.GetChild(1).gameObject;
    }
    
    /*
     * Spränger dörr om E klickas
     */
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E) && isDoorExplodable)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            AudioManager.instance.Play("DoorExplosion");
            AlertEnemies();

            Destroy(transform.parent.gameObject);
        }
    }

    /*
     * Visar när spelare kan spränga dörr
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isDoorExplodable)
        {
            EKey.SetActive(true);
        }
    }

    /*
     * Gömmer E-key
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isDoorExplodable)
        {
            EKey.SetActive(false);
        }
    }

    /*
     * Om enemy är i närheten så vänder sig de mot explosionen
     */
    private void AlertEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 15, enemyLayerMask);

        foreach (Collider2D enemy in enemies)
        {
            EnemyAI enemyAI = enemy.gameObject.GetComponent<EnemyAI>();

            IEnumerator coroutine = enemyAI.LookAtRoutine(transform.position);
            enemyAI.StartCoroutine(coroutine);
        }
    }
}
