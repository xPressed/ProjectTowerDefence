using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float health = 100f;
    public int money = 25;
    public GameObject dieEffect;

    [HideInInspector]
    public float startSpeed;

    private bool isDead = false;

    void Start()
    {
        startSpeed = speed;
    }
    
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float slowPercent)
    {
        speed = startSpeed * (1f - slowPercent);
    }

    void Die()
    {
        if (isDead)
            return;

        isDead = true;
        WaveSpawner.EnemiesAlive--;
        
        GameObject effectIns = (GameObject)Instantiate(dieEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
        
        PlayerStats.Money += money;
        Destroy(gameObject);
    }
}
