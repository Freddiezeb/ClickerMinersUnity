using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickaxeUpgrade : MonoBehaviour
{
    public float baseCost;
    public float upgradeCost;
    private float upgradeMultiplier;
    public float pickaxeLevel;
    public Text levelDisplay;
    public Text costDisplay;

	void Awake()
	{
		costDisplay.text = "$" + upgradeCost.ToString();
	}

    // Use this for initialization
    void Start()
    {
        upgradeMultiplier = 1.07f;
        //pickaxeLevel = globalStats.pickaxeLevel;
        if (pickaxeLevel > 1)
        {
            for (int i = 1; i < pickaxeLevel; i++)
            {
                upgradeCost += (pickaxeLevel * upgradeMultiplier);
            }
        }
    }

    public void IncreaseCurrency()
    {
        //GlobalClicks.currencyCount += (int)Math.Pow(pickaxeLevel, pickaxeLevel);
        GlobalClicks.currencyCount += (1 * (float)Math.Pow(1.5, pickaxeLevel - 1.0f));
    }

    public void UpgradePickaxe()
    {
        if (GlobalClicks.currencyCount >= upgradeCost)
        {
            pickaxeLevel++;
            levelDisplay.text = pickaxeLevel.ToString();
            GlobalClicks.currencyCount -= upgradeCost;
            UpgradeCostIncrease();
			costDisplay.text = "$" + upgradeCost.ToString();
        }
    }

    private void UpgradeCostIncrease()
    {
        //upgradeCost += (pickaxeLevel * upgradeMultiplier);
        upgradeCost += (baseCost * (float)Math.Pow(upgradeMultiplier, pickaxeLevel - 1.0f));
    }
}
