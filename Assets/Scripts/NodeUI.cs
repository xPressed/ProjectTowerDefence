using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject buttons;
    public TextMeshProUGUI upgrade;

    void Update()
    {
        if (target)
            if (target.isUpgraded)
                upgrade.SetText("Already\nUpgraded!");
    }
    
    public void SetTarget(Node node)
    {
        target = node;
        
        if (!target.isUpgraded)
            upgrade.SetText("Upgrade\n$" + target.turretBlueprint.upgradeCost);

        transform.position = target.GetBuildPosition();
        buttons.SetActive(true);
    }

    public void Hide()
    {
        buttons.SetActive(false);
    }
}
