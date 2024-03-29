﻿using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject particleBloodShot;
    [SerializeField] private GameObject particleBloodDead;
    [SerializeField] private GameObject particleHealthEffect;


    private void Start()
    {
        HUD.instance.SetHealth(GameData.PlayerHP);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }

    /* 
     * Lägg till amount HP
     * Spela bild effekt
     */
    public void Heal(int amount)
    {
        GameObject effect = Instantiate(particleHealthEffect);
        effect.GetComponent<FollowTarget>().target = transform;
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(effect, 1f);

        GameData.PlayerHP += amount;
        if (GameData.PlayerHP > GameData.MaxPlayerHP)
        {
            GameData.PlayerHP = GameData.MaxPlayerHP;
        }

        HUD.instance.SetHealth(GameData.PlayerHP);
    }

    /*
     * Ta bort amount från HP
     * Spela ljud och bild effekt
     */
    private void TakeDamage(int amount)
    {        
        Instantiate(particleBloodShot, transform.position, Quaternion.identity);

        GameData.PlayerHP -= amount;
        HUD.instance.SetHealth(GameData.PlayerHP);

        if (GameData.PlayerHP <= 0 && !GameData.isPlayerDead)
        {
            PlayerDead();
            return;
        }
        AudioManager.instance.Play("PlayerHurt");
    }

    /*
     * Spela ljud och bild effekt
     * Öppna DeathScreen
     */
    private void PlayerDead()
    {
        AudioManager.instance.Play("PlayerDeath");
        Instantiate(particleBloodDead, transform.position, Quaternion.identity);

        GameData.isPlayerDead = true;
        DeathScreen.instance.OnPlayerDead();
        Destroy(gameObject);
    }   
}