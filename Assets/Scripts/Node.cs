using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor = Color.yellow;
    public Color exceptionColor = Color.red;
    public Vector3 positionOffset;
    
    private Renderer rend;
    private Color startColor;

    [Header("Optional")]
    public GameObject turret;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    public bool isUpgraded = false;

    private BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        
        buildManager = BuildManager.instance;
        
        if (turret)
            Instantiate(turret, GetBuildPosition(), transform.rotation);
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        
        if (!buildManager.CanBuild)
            return;

        buildManager.BuildTurretOn(this);
    }
    
    void OnMouseEnter()
    {
        if (!buildManager.CanBuild || turret != null)
            return;

        if (!buildManager.HasMoney)
        {
            rend.material.color = exceptionColor;
            return;
        }
        
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
