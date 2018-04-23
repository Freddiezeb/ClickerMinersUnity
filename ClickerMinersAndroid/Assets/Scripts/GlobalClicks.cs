using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is the main script for the clicks, currencyCount can be reached from other scripts to add or remove $
public class GlobalClicks : MonoBehaviour
{
    public GameObject currencyDisplay;
    public static int currencyCount;
    private int internalCurrency;

    void Update()
    {
        internalCurrency = currencyCount;
        currencyDisplay.GetComponent<Text>().text = "$" + internalCurrency;
    }
}
