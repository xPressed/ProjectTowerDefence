using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;
    public TextMeshProUGUI currentMoney;

    public static int Lives;
    public int startLives = 20;
    public TextMeshProUGUI currentLives;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }

    void Update()
    {
        currentMoney.SetText("$" + Money.ToString());
        currentLives.SetText(Lives.ToString() + " LIVES LEFT");
    }
}
