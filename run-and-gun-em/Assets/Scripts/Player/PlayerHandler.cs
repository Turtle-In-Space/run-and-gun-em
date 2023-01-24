using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour
{
    private PlayerUI playerUI;
    private int playerHP = 10;


    private void Awake()
    {
        playerUI = GetComponent<PlayerUI>();
    }

    private void TakeDamage(int damage)
    {
        playerHP -= damage;
        playerUI.SetHealth(playerHP);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }
}
