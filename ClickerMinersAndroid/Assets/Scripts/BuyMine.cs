using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO - Change all costs to floats

//Handles all the mines to be bought
public class BuyMine : MonoBehaviour
{
    public int[] mineCost;

    public int[] minerCost;

    public float[] mineBonus;

    public int[] researchCosts;

	Mine gravelMine;
	Mine graniteMine;
	Mine metalMine;
	Mine obsidianMine;
	Mine goldMine;

	List<Mine> mineList;
	public List<Text> mineTextList;  //(btn...Mine)Texts for displaying the cost of each Mine

	public List<Text> minerTextList; //(btnBuyMiner)Texts for displaying the cost (index 0) and for the level (index 1) 
	public List<Text> researchTextList; //(btnUpgradeMiner)Texts for displaying the cost (index 0) and for the level (index 1) 

	Button btnBuyMiner;
	Button btnResearchUpgrade;

	MinerUpgrade minerUpgrade; //Script that creates the miners

	Image stoneImage; //the image of the stones

	public Sprite[] stoneSprites;

    TextFader textFader;

	void Start ()
	{
		Initialize ();
	}

    void BuyGravelMine()
    {
		HandleMine(gravelMine, 1);
    }

    void BuyGraniteMine()
    {
		HandleMine(graniteMine, 2);
    }

    void BuyMetalMine()
    {
		HandleMine(metalMine, 3);
    }

    void BuyObsidianMine()
    {
		HandleMine(obsidianMine, 0);
    }

    void BuyGoldMine()
    {
		HandleMine(goldMine, 0);
    }

	/// <summary>
	/// Buies the miner on click if its not unlocked.
	/// otherwise it just activates the canvas for displaying the btnResearchUpgrade
	/// </summary>
	void BuyMinerOnClick()
	{
		//TODO
		//Handle the logic of what happens when you press the buy miner button
	
		for (int i = 0; i < mineList.Count; i++) 
		{
			if (mineList [i].Active) 
			{
				if (!minerUpgrade.minerList [i].Unlocked) 
				{
					minerUpgrade.MinerPayment (minerUpgrade.minerList [i]);
					return;
				} 
				else if (minerUpgrade.minerList [i].Unlocked) 
				{
					minerUpgrade.ShowResearchCanvas (true);
				}
			}
		}
	}

	/// <summary>
	/// Miners the upgrade on click.
	/// </summary>
	void ResearchUpgradeOnClick()
	{
		//TODO
		//Handle the logic of what happens when you press the upgrade miner button 
		for (int i = 0; i < mineList.Count; i++) 
		{
			if (mineList [i].Active) 
			{
				minerUpgrade.ResearchUpgrade (minerUpgrade.minerList[i]);
				return;
			}
		}
	}


	/// <summary>
	/// The payment method for the mine.
	/// </summary>
	/// <param name="mine">Mine.</param>
	void MinePayment(Mine mine, int spriteIndex)
	{
		if (GlobalClicks.currencyCount >= mine.Cost)
		{
			GlobalClicks.currencyCount -= mine.Cost;
              PickaxeUpgrade.activeMineBonus = mine.MineBonus;
			mine.Unlocked = true;
			int index = mineList.FindIndex (i => i.Name == mine.Name);
			mineTextList[index].text = "Owned";
			mine.Active = true;
			HandelResearchCanvas (mine);
			minerUpgrade.ShowMinerCanvas (true);
			setStoneSprite (spriteIndex);
		}
		else
		{
            textFader.DisplayText(GlobalItems.displayTimes[1], GlobalItems.displayTexts[0]);
		}
	}

	/// <summary>
	/// Handels if the mine is Unlocked or not.
	/// </summary>
	/// <param name="mine">Mine.</param>
	void HandleMine(Mine mine, int spriteIndex)
	{
		UnActivateMine (mine);
		if (!mine.Unlocked) 
		{
			MinePayment (mine, spriteIndex);
		} 
		else if(mine.Unlocked) 
		{
			if (mine.Active)
			{
				mine.Active = false;
                PickaxeUpgrade.activeMineBonus = 0f;
				minerUpgrade.ShowMinerCanvas (false);
				setStoneSprite (0);
			} 
			else 
			{
				mine.Active = true;
                PickaxeUpgrade.activeMineBonus = mine.MineBonus;
				HandelResearchCanvas (mine);
				minerUpgrade.ShowMinerCanvas (true);
				setStoneSprite (spriteIndex);
			}
		}
		Debug.Log (mine.Name + " Active: " + mine.Active.ToString ());
	}

	void HandelResearchCanvas(Mine mine)
	{
		int index = mineList.FindIndex (i => i.Name == mine.Name);
//		Debug.Log (index);
		if (minerUpgrade.minerList [index].Unlocked)
		{
			minerUpgrade.ShowResearchCanvas (true);
			minerTextList[0].text = "";
			minerTextList[1].text = "Owned";
			minerUpgrade.SetResearchText (minerUpgrade.minerList [index]);
		} 
		else if (!minerUpgrade.minerList [index].Unlocked) 
		{
			minerUpgrade.ShowResearchCanvas (false);
			minerTextList[0].text = "$ " + minerUpgrade.minerList[index].Cost;
			minerTextList[1].text = "";
			minerUpgrade.SetResearchText (minerUpgrade.minerList [index]);
		}
	}


	/// <summary>
	/// Unactivate mine all mines exept the current one if it is true.
	/// </summary>
	/// <param name="currentMine">Current mine.</param>
	void UnActivateMine(Mine currentMine)
	{
		bool isActive = currentMine.Active;
		for (int i = 0; i < mineList.Count; i++) 
		{
			mineList [i].Active = false;
		}
		currentMine.Active = isActive;
	}

	/// <summary>
	/// Sets the stone sprite.
	/// </summary>
	/// <param name="spriteIndex">Sprite index.</param>
	void setStoneSprite (int spriteIndex) 
	{
		stoneImage.sprite = stoneSprites [spriteIndex];
	}

	/// <summary>
	/// Initialize resources.
	/// </summary>
	void Initialize()
	{
		GameObject temp = GameObject.Find ("btnBuyMiner");
		if (temp != null) 
		{
			btnBuyMiner = temp.GetComponent<Button> ();
			btnBuyMiner.onClick.AddListener (BuyMinerOnClick);
			Debug.Log ("btnBuyMiner is initialized");
		}
		else 
		{
			Debug.Log ("Could not find btnBuyMiner in Scene");
		}

		temp = GameObject.Find ("btnResearchUpgrade");
		if (temp != null) 
		{
			btnResearchUpgrade = temp.GetComponent<Button> ();
			btnResearchUpgrade.onClick.AddListener (ResearchUpgradeOnClick);
			Debug.Log ("btnMinerUpgrade is initialized");
		} 
		else 
		{
			Debug.Log ("Could not find btnUpgradeMiner in Scene");
		}

		gravelMine = new Mine ("gravelMine", mineCost[0], mineBonus[0]);
        graniteMine = new Mine("graniteMine", mineCost[1], mineBonus[1]);
        metalMine = new Mine("metalMine", mineCost[2], mineBonus[2]);
        obsidianMine = new Mine("obsidianMine", mineCost[3], mineBonus[3]);
        goldMine = new Mine("goldMine", mineCost[4], mineBonus[4]);

		mineList = new List<Mine>();
		mineList.Add (gravelMine);
		mineList.Add (graniteMine);
		mineList.Add (metalMine);
		mineList.Add (obsidianMine);
		mineList.Add (goldMine); 

		minerUpgrade = new MinerUpgrade (minerTextList, researchTextList, minerCost, researchCosts);

		temp = GameObject.Find ("stone");
		if (temp != null) 
		{
			stoneImage = temp.GetComponent<Image>();
			setStoneSprite (0);
			Debug.Log ("spriteRenderer is initialized");
		} 
		else 
		{
			Debug.Log ("Could not find stone in Scene");
		}

        temp = GameObject.Find("ClickMechanic");
        if (temp != null)
        {
            textFader = temp.GetComponent<TextFader>();
            Debug.Log("TextFader is initialized");
        }
        else
        {
            Debug.Log("Could not find TextFader in Scene");
        }
	}
}

public class Mine
{
	string name;
	int cost;
	bool unlocked = false;
	bool active = false;
    float mineBonus = 0;

	public string Name { get { return name; } }

	public int Cost { get { return cost; } }

	public bool Unlocked { get { return unlocked; } set { unlocked = value; } }

	public bool Active { get { return active; } set { active = value; } }

    public float MineBonus { get { return mineBonus; } }

	/// <summary>
	/// Initializes a new instance of the <see cref="Mine"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="cost">Cost.</param>
	public Mine(string name, int cost, float mineBonus)
	{
		this.name = name;
		this.cost = cost;
        this.mineBonus = mineBonus;
	}
}
