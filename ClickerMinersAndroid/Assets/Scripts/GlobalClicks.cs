using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is the main script for the clicks, currencyCount can be reached from other scripts to add or remove $
public class GlobalClicks : MonoBehaviour
{
    public GameObject currencyDisplay;
    public static double currencyCount;
//    private int internalCurrency;
	private Text displayText;

	void Start()
	{
		displayText = currencyDisplay.GetComponent<Text> (); 
	}

    void Update()
    {
//        internalCurrency = currencyCount;
		displayText.text = "$" + currencyCount;
    }

	/// <summary>
	/// Calculates the current price for the object.
	/// </summary>
	/// <param name="startPrice">Start price of the object.</param>
	/// <param name="level">The current level of the object.</param>
	/// <param name="increase">Increasement in procent (1.1 = 10% increase).</param>
	void CalculateCurrentPrice(int startPrice, int level, float increase)
	{
		//equation   n..
        
	}
}
