using System.Collections;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public bool canExplode = false;

    [SerializeField] private GameObject explosionPrefab;

    private GameObject EKey;

    private readonly int enemyLayerMask = 1 << 8;


    private void Awake()
    {
        EKey = transform.parent.GetChild(1).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canExplode)
        {
            EKey.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) && canExplode && collision.CompareTag("Player"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            AudioManager.instance.Play("DoorExplosion");
            AlertEnemies();

            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canExplode)
        {
            EKey.SetActive(false);
        }
    }

    private void AlertEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 15, enemyLayerMask);

        foreach(Collider2D enemy in enemies)
        {
            EnemyAI enemyAI = enemy.gameObject.GetComponent<EnemyAI>();
            IEnumerator coroutine = enemyAI.LookAtRoutine(transform.position);
            enemyAI.StartCoroutine(coroutine);
        }
    }
}
