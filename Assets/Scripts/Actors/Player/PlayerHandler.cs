﻿using UnityEngine;
using System;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private GameObject particleBloodShot;
    [SerializeField] private GameObject particleBloodDead;
    [SerializeField] private GameObject particleHealthEffect;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {        
        GameObject blood = Instantiate(particleBloodShot, transform.position, Quaternion.identity);
        Destroy(blood, 3f);

        GameData.PlayerHP -= damage;
        HUD.instace.SetHealth(GameData.PlayerHP);

        if (GameData.PlayerHP <= 0 && !GameData.isPlayerDead)
        {
            Dead();
            return;
        }
        AudioManager.instance.Play("PlayerHurt");
    }

    private void Dead()
    {
        AudioManager.instance.Play("PlayerDeath");
        GameObject blood = Instantiate(particleBloodDead, transform.position, Quaternion.identity);
        Destroy(blood, 3f);

        GameData.isPlayerDead = true;
        DeathScreen.instace.Dead();
        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        GameObject effect = Instantiate(particleHealthEffect);
        effect.GetComponent<FollowTarget>().target = transform;
        effect.GetComponent<ParticleSystem>().Play();

        GameData.PlayerHP += amount;
        if (GameData.PlayerHP > GameData.MaxPlayerHP)
            GameData.PlayerHP = GameData.MaxPlayerHP;

        HUD.instace.SetHealth(GameData.PlayerHP);
    }
}