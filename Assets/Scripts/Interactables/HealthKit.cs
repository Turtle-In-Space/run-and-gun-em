using UnityEngine;

public class HealthKit : MonoBehaviour
{
    private Player player;
    private readonly int amountofHP = 2;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.Heal(amountofHP);
            AudioManager.instance.Play("HealthKit");
            Destroy(gameObject);
        }
    }
}
