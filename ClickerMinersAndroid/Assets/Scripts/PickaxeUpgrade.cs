using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickaxeUpgrade : MonoBehaviour
{

    public int upgradeCost;
    public int upgradeMultiplier;
    public int pickaxeLevel;
    public Text levelDisplay;
    public Text costDisplay;

	void Awake()
	{
		costDisplay.text = "$" + upgradeCost.ToString();
	}

    // Use this for initialization
    void Start()
    {
        //pickaxeLevel = globalStats.pickaxeLevel;
        if (pickaxeLevel > 1)
        {
            for (int i = 1; i < pickaxeLevel; i++)
            {
                upgradeCost += (pickaxeLevel * upgradeMultiplier);
            }
        }
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
        upgradeCost += (pickaxeLevel * upgradeMultiplier);
    }
}
