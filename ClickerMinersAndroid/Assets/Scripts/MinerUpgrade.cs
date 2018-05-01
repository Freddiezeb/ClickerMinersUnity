using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinerUpgrade : MonoBehaviour
{
	Canvas minerCanvas;

	public Miner gravelMiner;
	public Miner graniteMiner;
	public Miner metalMiner;
	public Miner obsidianMiner;
	public Miner goldMiner;

	public float[] researchCosts;
	public float[] minerCost;

	public List<Text> minerTextList;
	public List<Text> researchTextList;
	public List<Miner> minerList;

	Canvas researchCanvas;

    TextFader textFader;

	public Button btnBuyMiner;
	public Button btnResearchUpgrade;

	void Awake ()
	{
		Initialize ();
	}

	/// <summary>
	/// Shows the canvas.
	/// </summary>
	/// <param name="show">If set to <c>true</c> show.</param>
	public void ShowMinerCanvas(bool show)
	{
		ShowCanvas (minerCanvas, show);
	}

	/// <summary>
	/// Shows the canvas.
	/// </summary>
	/// <param name="show">If set to <c>true</c> show.</param>
	public void ShowResearchCanvas(bool show)
	{
		ShowCanvas (researchCanvas, show);
	}

	void ShowCanvas(Canvas canvas, bool show)
	{
		if (canvas != null) 
		{
			canvas.gameObject.SetActive (show);
		}
	}

	/// <summary>
	/// Initialize resources.
	/// </summary>
	/// <param name="minerCostList">Miner cost list.</param>
	void Initialize()
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

		gravelMiner = new Miner ("gravelMiner", minerCost[0], researchCosts[0]);
        graniteMiner = new Miner("graniteMiner", minerCost[1], researchCosts[1]);
        metalMiner = new Miner("metalMiner", minerCost[2], researchCosts[2]);
        obsidianMiner = new Miner("obsidianMiner", minerCost[3], researchCosts[3]);
        goldMiner = new Miner("goldMiner", minerCost[4], researchCosts[4]);

		minerList = new List<Miner> ();
		minerList.Add (gravelMiner);
		minerList.Add (graniteMiner);
		minerList.Add (metalMiner);
		minerList.Add (obsidianMiner);
		minerList.Add (goldMiner);

		temp = GameObject.Find ("ResearchCanvas");
		if (temp != null) 
		{
			researchCanvas = temp.GetComponent<Canvas> ();
			researchCanvas.gameObject.SetActive (false);
			Debug.Log ("ResearchCanvas is initialized");
		} 
		else 
		{
			Debug.Log ("Could not find ResearchCanvas in Scene");
		}

		temp = GameObject.Find ("MinerCanvas");
		if (temp != null) 
		{
			minerCanvas = temp.GetComponent<Canvas> ();
			minerCanvas.gameObject.SetActive (false);
			Debug.Log ("MinerCanvas is initialized");
		} 
		else 
		{
			Debug.Log ("Could not find MinerCanvas in Scene");
		}
	}

	public void MinerPayment(Miner miner)
	{
		if (GlobalClicks.currencyCount >= miner.Cost) 
		{
			GlobalClicks.currencyCount -= miner.Cost;
			miner.Unlocked = true;
			minerTextList[0].text = "";
			minerTextList[1].text = "Owned";
			ShowCanvas (researchCanvas, true);
			SetResearchText (miner);
		}
        else
        {
            textFader.DisplayText(GlobalItems.displayTimes[1], GlobalItems.displayTexts[0]);
        }
	}

	public void ResearchUpgrade(Miner miner)
	{
		if (GlobalClicks.currencyCount >= miner.ResearchCost) 
		{
			GlobalClicks.currencyCount -= miner.ResearchCost;
			miner.ResearchLevel++;
			IncreaseResearchCost(miner);
			SetResearchText (miner);
		}
        else
        {
            textFader.DisplayText(GlobalItems.displayTimes[1], GlobalItems.displayTexts[0]);
        }
	}

	void IncreaseResearchCost(Miner miner)
	{
		//Calculate the new price
        miner.ResearchCost += GlobalItems.CalculateNewPrice(miner.ResearchStartCost, 1.15f, miner.ResearchLevel);
	}

	public void SetResearchText(Miner miner)
	{
		researchTextList[0].text = "$" + miner.ResearchCost.ToString();
		researchTextList[1].text = miner.ResearchLevel.ToString();
	}
}

public class Miner
{
	string name;
	float cost;
	bool unlocked = false;
	int researchLevel;
	float researchCost;
	float researchStartCost;

	public string Name { get { return name; } set { name = value; } }

	public float Cost { get { return cost; } set { cost = value; } }

	public bool Unlocked { get { return unlocked; } set { unlocked = value; } }

	public int ResearchLevel { get { return researchLevel; } set { researchLevel = value; } }

	public float ResearchCost { get { return researchCost; } set { researchCost = value; } }

	public float ResearchStartCost { get { return researchStartCost; } }

	/// <summary>
	/// Initializes a new instance of the <see cref="Miner"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="cost">Cost.</param>
	public Miner(string name, float cost, float researchCost)
	{
		this.name = name;
		this.cost = cost;
        this.researchCost = researchCost;
        researchStartCost = researchCost;
		researchLevel = 1;
	}
}
