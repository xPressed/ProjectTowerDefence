using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }
    
    private TurretBlueprint turretToBuild;
    public GameObject buildEffect;
    public GameObject destroyEffect;

    private Node selectedNode;
    public NodeUI nodeUI;

    public bool CanBuild => turretToBuild != null;
    public bool HasMoney => PlayerStats.Money >= turretToBuild.cost;

    public void SelectTurretToBuild(TurretBlueprint turretBlueprint)
    {
        turretToBuild = turretBlueprint;

        DeselectNode();
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            turretToBuild = null;
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition() + Vector3.up * -turretToBuild.turretOffset, node.transform.rotation);
        node.turret = turret;
        node.turretBlueprint = turretToBuild;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        turretToBuild = null;
    }
    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void UpgradeTurret()
    {
        if (selectedNode.isUpgraded)
        {
            Debug.Log("Turret is already upgraded!");
            return;
        }

        if (PlayerStats.Money < selectedNode.turretBlueprint.upgradeCost)
        {
            Debug.Log("Not Enough Money!");
            return;
        }

        PlayerStats.Money -= selectedNode.turretBlueprint.upgradeCost;
        
        Destroy(selectedNode.turret);
        GameObject turret = (GameObject)Instantiate(selectedNode.turretBlueprint.upgradedPrefab, selectedNode.GetBuildPosition() + Vector3.up * -selectedNode.turretBlueprint.turretOffset, selectedNode.transform.rotation);
        selectedNode.turret = turret;
        selectedNode.isUpgraded = true;

        GameObject effect = (GameObject)Instantiate(buildEffect, selectedNode.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void SellTurret()
    {
        PlayerStats.Money += selectedNode.turretBlueprint.cost / 2;
        Destroy(selectedNode.turret);
        selectedNode.turret = null;
        selectedNode.isUpgraded = false;
        
        GameObject effect = (GameObject)Instantiate(destroyEffect, selectedNode.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        
        DeselectNode();
    }
}
