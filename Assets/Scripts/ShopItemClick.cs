using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemClick : MonoBehaviour
{
    public Shop shop;
    public bool StandardTurret;
    public bool MissileLauncher;
    public bool LaserBeamer;

    void OnMouseDown()
    {
        if (StandardTurret)
        {
            shop.SelectStandardTurret();   
            return;
        }

        if (MissileLauncher)
        {
            shop.SelectMissileLauncher();
            return;
        }

        if (LaserBeamer)
        {
            shop.SelectLaserBeamer();
        }
    }
}
