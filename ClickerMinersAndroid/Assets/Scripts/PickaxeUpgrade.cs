using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickaxeUpgrade : MonoBehaviour
{
    public int baseCost;
    public int pickaxeLevel;
    public float upgradeCost;
    public float incomeMultiplier;
    public float upgradeMultiplier;

    public Text levelDisplay;
    public Text costDisplay;

    public static float activeMineBonus = 0;

    TextFader textFader;

    public ParticleSystem sparkParticle;

	void Awake()
	{
		costDisplay.text = "$" + upgradeCost.ToString();
	}

    // Use this for initialization
    void Start()
    {
        GameObject temp = GameObject.Find("ClickMechanic");
        if (temp != null)
        {
            textFader = temp.GetComponent<TextFader>();
            Debug.Log("TextFader is initialized");
        }
        else
        {
            Debug.Log("Could not find TextFader in Scene");
        }
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
        sparkParticle.Play();
        GlobalClicks.currencyCount += CalculateIncreasedCurrency(incomeMultiplier, pickaxeLevel, activeMineBonus);
    }

    private float CalculateIncreasedCurrency(float multiplier, int level, float mineBonus)
    {
        float temp = 0f;
        temp += (float)(1 * (float)Math.Pow(multiplier, level - 1.0f));
        temp = temp * (1f + mineBonus);
        return temp;
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
        else
        {
            textFader.DisplayText(GlobalItems.displayTimes[1], GlobalItems.displayTexts[0]);
        }
    }

    private void UpgradeCostIncrease()
    {
        upgradeCost += GlobalItems.CalculateNewPrice(baseCost, upgradeMultiplier, pickaxeLevel);
    }
}
