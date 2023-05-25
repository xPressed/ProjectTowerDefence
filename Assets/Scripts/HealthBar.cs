using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform cam;
    public float turnSpeed = 5f;
    
    public Enemy enemy;
    private float enemyMaxHealth;

    public GameObject background;
    public GameObject panel;


    void Start()
    {
        cam = Camera.main.transform;
        enemyMaxHealth = enemy.health;
        background.SetActive(false);
    }
    
    void Update()
    {
        RotateToCam();
        UpdateHealth();
    }

    void RotateToCam()
    {
        Vector3 dir = transform.position - cam.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation);
    }

    void UpdateHealth()
    {
        if (enemy.health >= enemyMaxHealth)
            return;
        
        background.SetActive(true);
        Vector3 rescale = panel.transform.localScale;
        rescale.x = (enemy.health / enemyMaxHealth) * 2;
        panel.transform.localScale = rescale;
    }
}
