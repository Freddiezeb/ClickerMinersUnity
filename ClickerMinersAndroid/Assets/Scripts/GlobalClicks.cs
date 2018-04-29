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

	void Start()
	{
		displayText = currencyDisplay.GetComponent<Text> (); 
	}

    void Update()
    {
		displayText.text = "$" + currencyCount;
    }
}
