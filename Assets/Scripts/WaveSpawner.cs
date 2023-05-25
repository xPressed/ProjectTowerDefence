using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    
    public Wave[] waves;
    private int waveIndex = 0;
    
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    public float countdown = 2f;
    public TextMeshProUGUI waveCountdownText; 
    
    public GameManager gameManager;

    void Start()
    {
        EnemiesAlive = 0;
    }
    void Update()
    {
        if (waveIndex == waves.Length && EnemiesAlive == 0)
        {
            gameManager.WinLevel();
            enabled = false;
            return;
        }

        if (EnemiesAlive > 0)
            return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.SetText($"{countdown:00.00}".Replace(',', '.'));
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.enemyAmount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        EnemiesAlive++;
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
