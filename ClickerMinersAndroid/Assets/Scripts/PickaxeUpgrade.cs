using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickaxeUpgrade : MonoBehaviour
{
    public float baseCost;
    public int pickaxeLevel;
    public float upgradeCost;
    public float incomeMultiplier;
    public float upgradeMultiplier;
    private float displayPickaxeIncome;

    private float calculatedIncrease;

    public Text levelDisplay;
    public Text costDisplay;
    public Text pickaxeIncome;

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

    void Update()
    {
        if (RoundUp(calculatedIncrease, 1) < 500)
        {
            displayPickaxeIncome = RoundUp(calculatedIncrease, 10);
        }
        else
        {
            displayPickaxeIncome = RoundUp(calculatedIncrease, 1);
        }
        pickaxeIncome.text = "$" + displayPickaxeIncome;
    }

    public double RoundUp(double number, float precision)
    {
        double temp = number * precision;
        int tempint = (int)temp;
        return tempint / precision;
    }

    public void IncreaseCurrency(float skillMultiplier)
    {
        calculatedIncrease = (CalculateIncreasedCurrency(incomeMultiplier, pickaxeLevel, activeMineBonus)) * skillMultiplier;
        //calculatedIncrease = RoundUp(calculatedIncrease, 100);
        GlobalClicks.currencyCount += calculatedIncrease;
        sparkParticle.Play();
    }

    public float RoundUp(float number, float precision)
    {
        ////calculatedIncrease = (int)(calculatedIncrease + 0.5f);
        //int factor = (float)(Math.Pow(10, precision));
        //return Math.Round(number * factor) / factor;
        float temp = number * precision;
        int tempint = (int)temp;
        return tempint / precision;
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
        float temp = RoundUp(GlobalItems.CalculateNewPrice(baseCost, upgradeMultiplier, pickaxeLevel), 10);
        upgradeCost += temp;
    }
}
