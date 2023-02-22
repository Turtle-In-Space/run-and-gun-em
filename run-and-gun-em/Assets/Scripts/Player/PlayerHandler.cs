using UnityEngine;
using System;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private GameObject particleBloodShot;
    [SerializeField] private GameObject particleBloodDead;

    private void TakeDamage(int damage)
    {
        GameObject blood = Instantiate(particleBloodShot, transform.position, Quaternion.identity);
        Destroy(blood, 3f);

        GameData.PlayerHP -= damage;
        HUD.instace.SetHealth(GameData.PlayerHP);

        if (GameData.PlayerHP <= 0 && !GameData.isPlayerDead)
        {
            Dead();
        }
    }

    private void Dead()
    {
        GameObject blood = Instantiate(particleBloodDead, transform.position, Quaternion.identity);
        Destroy(blood, 3f);

        GameData.isPlayerDead = true;
        DeathScreen.instace.Dead();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            //print("Take no damage");
        }
    }
}