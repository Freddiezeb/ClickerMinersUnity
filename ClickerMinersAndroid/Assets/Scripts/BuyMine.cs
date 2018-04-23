using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles all the mines to be bought
public class BuyMine : MonoBehaviour
{
    public Text textBox;
    public int gravelMineCost;
    public int graniteMineCost;
    public int metalMineCost;
    public int obsidianMineCost;
    public int goldMineCost;

    public void MinePayment(int mineCost)
    {
        if (GlobalClicks.currencyCount >= mineCost)
        {
            GlobalClicks.currencyCount -= mineCost;
        }
        else
        {
            StartCoroutine(FadeTextToZeroAlpha(2f, textBox));
        }
    }

    public void BuyGravelMine()
    {
        MinePayment(gravelMineCost);
    }

    public void BuyGraniteMine()
    {
        MinePayment(graniteMineCost);
    }

    public void BuyMetalMine()
    {
        MinePayment(metalMineCost);
    }

    public void BuyObsidianMine()
    {
        MinePayment(obsidianMineCost);
    }

    public void BuyGoldMine()
    {
        MinePayment(goldMineCost);
    }


    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
