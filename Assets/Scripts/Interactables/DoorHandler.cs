using System.Collections;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    public bool canExplode = false;

    private readonly int enemyLayerMask = 1 << 8;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canExplode)
        {
            transform.parent.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) && canExplode)
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
