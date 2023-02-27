using UnityEngine;

public class HealthKit : MonoBehaviour
{
    private PlayerHandler playerHandler;
    private readonly int amountofHP = 2;


    private void Start()
    {
        playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerHandler.Heal(amountofHP);
            Destroy(gameObject);
        }
    }
}
