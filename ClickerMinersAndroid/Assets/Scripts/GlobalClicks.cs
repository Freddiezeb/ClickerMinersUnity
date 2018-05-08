using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is the main script for the clicks, currencyCount can be reached from other scripts to add or remove $
public class GlobalClicks : MonoBehaviour
{
    public GameObject currencyDisplay;
    public static double currencyCount;
    private Text displayText;

    private double displayCurrency;
    double temp = 500;

    void Start()
    {
        displayText = currencyDisplay.GetComponent<Text>();
    }

    void Update()
    {
        if (RoundUp(currencyCount, 1) < 500)
        {
            displayCurrency = RoundUp(currencyCount, 10);
        }
        else
        {
            displayCurrency = RoundUp(currencyCount, 1);
        }
            displayText.text = "$" + displayCurrency;
    }

    public double RoundUp(double number, float precision)
    {
        double temp = number * precision;
        int tempint = (int)temp;
        return tempint / precision;
    }
}
