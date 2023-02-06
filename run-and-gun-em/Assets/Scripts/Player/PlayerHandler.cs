using UnityEngine;
using System;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField]private HUD playerHUD;    
    private DeathScreen deathScreen;


    private void Start()
    {
        deathScreen = GameObject.Find("DeathScreen").GetComponent<DeathScreen>();
    }

    private void TakeDamage(int damage)
    {
        GameData.PlayerHP -= damage;
        playerHUD.SetHealth(GameData.PlayerHP);

        if (GameData.PlayerHP <= 0 && !GameData.isPlayerDead)
        {
            deathScreen.Dead();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }
}