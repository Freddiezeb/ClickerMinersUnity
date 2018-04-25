using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinersUpgrade {
	Canvas canvas;

	Button btnBuyMiner;
	Button btnUpgradeMiner;

	public Miner gravelMiner;
	public Miner graniteMiner;
	public Miner metalMiner;
	public Miner obsidianMiner;
	public Miner goldMiner;

	/// <summary>
	/// Initializes a new instance of the <see cref="MinersUpgrade"/> class.
	/// </summary>
	public MinersUpgrade (List<int> minerCostList) {
		Initialize (minerCostList);
	}

	/// <summary>
	/// Shows the canvas.
	/// </summary>
	/// <param name="show">If set to <c>true</c> show.</param>
	public void ShowCanvas(bool show){
		if (canvas != null) {
			canvas.gameObject.SetActive (show);
		}
	}

	/// <summary>
	/// Buies the miner on click.
	/// </summary>
	void BuyMinerOnClick(){
		//TODO
		//Handle the logic of what happens when you press the buy miner button 
	}

	/// <summary>
	/// Miners the upgrade on click.
	/// </summary>
	void UpgradeMinerOnClick(){
		//TODO
		//Handle the logic of what happens when you press the upgrade miner button 
	}

	/// <summary>
	/// Initialize resources.
	/// </summary>
	/// <param name="minerCostList">Miner cost list.</param>
	void Initialize(List<int> minerCostList){
		SetComponents ();

		gravelMiner = new Miner ("gravelMiner", minerCostList[0]);
		graniteMiner = new Miner ("graniteMiner", minerCostList[1]);
		metalMiner = new Miner ("metalMiner", minerCostList[2]);
		obsidianMiner = new Miner ("obsidianMiner", minerCostList[3]);
		goldMiner = new Miner ("goldMiner", minerCostList[4]);
	}

	/// <summary>
	/// Fetches and sets the components. (Buttons, Canvas mm)
	/// </summary>
	void SetComponents(){
		GameObject temp = GameObject.Find ("btnBuyMiner");
		if (temp != null) {
			btnBuyMiner = temp.GetComponent<Button> ();
			btnBuyMiner.onClick.AddListener (BuyMinerOnClick);
			Debug.Log ("btnBuyMiner is initialized");
		} else {
			Debug.Log ("Could not find btnBuyMiner in Scene");
		}

		temp = GameObject.Find ("btnUpgradeMiner");
		if (temp != null) {
			btnUpgradeMiner = temp.GetComponent<Button> ();
			btnUpgradeMiner.onClick.AddListener (UpgradeMinerOnClick);
			Debug.Log ("btnMinerUpgrade is initialized");
		} else {
			Debug.Log ("Could not find btnUpgradeMiner in Scene");
		}
		temp = GameObject.Find ("MinerCanvas");
		if (temp != null) {
			canvas = temp.GetComponent<Canvas> ();
			canvas.gameObject.SetActive (false);
			Debug.Log ("MinerCanvas is initialized");
		} else {
			Debug.Log ("Could not find MinerCanvas in Scene");
		}
	}

//TODO!
	//försöka göra uppdateringen generic för alla miners
}

public class Miner
{
	string name;
	int cost;
	bool unlocked = false;

	public string Name { get { return name; } set { name = value; } }

	public int Cost { get { return cost; } set { cost = value; } }

	public bool Unlocked { get { return unlocked; } set { unlocked = value; } }

	/// <summary>
	/// Initializes a new instance of the <see cref="Miner"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="cost">Cost.</param>
	public Miner(string name, int cost)
	{
		this.name = name;
		this.cost = cost;
	}
}