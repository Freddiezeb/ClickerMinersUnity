using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinerUpgrade {
	Canvas minerCanvas;

	public Miner gravelMiner;
	public Miner graniteMiner;
	public Miner metalMiner;
	public Miner obsidianMiner;
	public Miner goldMiner;

	List<Text> minerTextList;
	List<Text> researchTextList;
	public List<Miner> minerList;

	Canvas researchCanvas;

	/// <summary>
	/// Initializes a new instance of the <see cref="MinersUpgrade"/> class.
	/// </summary>
	public MinerUpgrade (List<Text> minerTextList, List<Text> researchTextList, List<int> minerCostList) 
	{
		this.minerTextList = minerTextList;
		this.researchTextList = researchTextList;
		Initialize (minerCostList);
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
	void Initialize(List<int> minerCostList)
	{
		GameObject temp = GameObject.Find ("ResearchCanvas");
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

		gravelMiner = new Miner ("gravelMiner", minerCostList[0]);
		graniteMiner = new Miner ("graniteMiner", minerCostList[1]);
		metalMiner = new Miner ("metalMiner", minerCostList[2]);
		obsidianMiner = new Miner ("obsidianMiner", minerCostList[3]);
		goldMiner = new Miner ("goldMiner", minerCostList[4]);

		minerList = new List<Miner> ();
		minerList.Add (gravelMiner);
		minerList.Add (graniteMiner);
		minerList.Add (metalMiner);
		minerList.Add (obsidianMiner);
		minerList.Add (goldMiner);
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
	}

	public void ResearchUpgrade(Miner miner)
	{
		if (GlobalClicks.currencyCount >= miner.ResearchCost) 
		{
			GlobalClicks.currencyCount -= miner.ResearchCost;
			miner.ResearchLevel++;
			SetResearchText (miner);
			IncreaseResearchCost (miner.ResearchCost);
		}
	}

	void IncreaseResearchCost(int currentCost)
	{
		//Calculate the new price
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
	int cost;
	bool unlocked = false;
	int researchLevel;
	int researchCost;

	public string Name { get { return name; } set { name = value; } }

	public int Cost { get { return cost; } set { cost = value; } }

	public bool Unlocked { get { return unlocked; } set { unlocked = value; } }

	public int ResearchLevel { get { return researchLevel; } set { researchLevel = value; } }

	public int ResearchCost { get { return researchCost; } set { researchCost = value; } }

	/// <summary>
	/// Initializes a new instance of the <see cref="Miner"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="cost">Cost.</param>
	public Miner(string name, int cost)
	{
		this.name = name;
		this.cost = cost;
		researchLevel = 0;
	}
}