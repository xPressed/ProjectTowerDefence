using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;

    public GameObject gameOverUI;
    public GameObject winUI;

    public int level;

    void Start()
    {
        gameIsOver = false;
    }

    void Update()
    {
        if (gameIsOver)
            return;
        
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Time.timeScale = 0f;
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        Time.timeScale = 0f;
        PlayerPrefs.SetInt("levelReached", level);
        winUI.SetActive(true);
    }
}
