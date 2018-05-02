using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles all the mines to be bought
public class BuyMine : MonoBehaviour
{
    public float[] mineCost;

    public float[] mineBonus;

	Mine gravelMine;
	Mine graniteMine;
	Mine metalMine;
	Mine obsidianMine;
	Mine goldMine;

	List<Mine> mineList;
	public List<Text> mineTextList;  //(btn...Mine)Texts for displaying the cost of each Mine

	MinerUpgrade minerUpgrade; //Script that creates the miners

	Image stoneImage; //the image of the stones

	Image stoneBkImage;

	public Sprite[] stoneSprites;

	public Sprite[] stoneBkSprites;

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
		HandleMine(obsidianMine, 4);
    }

    void BuyGoldMine()
    {
		HandleMine(goldMine, 5);
    }

	/// <summary>
	/// Buies the miner on click if its not unlocked.
	/// otherwise it just activates the canvas for displaying the btnResearchUpgrade
	/// </summary>
	void BuyMinerOnClick()
	{
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
			mineTextList[index].text = mine.Name;
			mine.Active = true;
			HandelResearchCanvas (mine);
			minerUpgrade.ShowMinerCanvas (true);
			setStoneSprite (spriteIndex);
			setStoneBkSprite (spriteIndex);
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
				setStoneBkSprite (0);
			} 
			else 
			{
				mine.Active = true;
                PickaxeUpgrade.activeMineBonus = mine.MineBonus;
				HandelResearchCanvas (mine);
				minerUpgrade.ShowMinerCanvas (true);
				setStoneSprite (spriteIndex);
				setStoneBkSprite (spriteIndex);
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
			minerUpgrade.minerTextList[0].text = "";
			minerUpgrade.minerTextList[1].text = "Owned";
			minerUpgrade.SetResearchText (minerUpgrade.minerList [index]);
		} 
		else if (!minerUpgrade.minerList [index].Unlocked) 
		{
			minerUpgrade.ShowResearchCanvas (false);
			minerUpgrade.minerTextList[0].text = "$ " + minerUpgrade.minerList[index].Cost;
			minerUpgrade.minerTextList[1].text = "";
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
	/// Sets the stone sprite.
	/// </summary>
	/// <param name="spriteIndex">Sprite index.</param>
	void setStoneBkSprite (int spriteIndex) 
	{
		stoneBkImage.sprite = stoneBkSprites [spriteIndex];
	}

	/// <summary>
	/// Initialize resources.
	/// </summary>
	void Initialize()
	{
		gravelMine = new Mine ("GRAVEL", mineCost[0], mineBonus[0]);
        graniteMine = new Mine("GRANITE", mineCost[1], mineBonus[1]);
        metalMine = new Mine("METAL", mineCost[2], mineBonus[2]);
        obsidianMine = new Mine("OBSIDIAN", mineCost[3], mineBonus[3]);
        goldMine = new Mine("GOLD", mineCost[4], mineBonus[4]);

		mineList = new List<Mine>();
		mineList.Add (gravelMine);
		mineList.Add (graniteMine);
		mineList.Add (metalMine);
		mineList.Add (obsidianMine);
		mineList.Add (goldMine); 

		GameObject temp = GameObject.Find ("stone");
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

		temp = GameObject.Find ("iStones");
		if (temp != null) 
		{
			stoneBkImage = temp.GetComponent<Image>();
			setStoneBkSprite (0);
			Debug.Log ("iStones is initialized");
		} 
		else 
		{
			Debug.Log ("Could not find iStones in Scene");
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

		temp = GameObject.Find("MineObjects");
		if (temp != null)
		{
			minerUpgrade = temp.GetComponent<MinerUpgrade>();
			minerUpgrade.btnBuyMiner.onClick.AddListener (BuyMinerOnClick);
			minerUpgrade.btnResearchUpgrade.onClick.AddListener (ResearchUpgradeOnClick);
			Debug.Log("MinerUpgrade is initialized");
		}
		else
		{
			Debug.Log("Could not find MinerUpgrade in Scene");
		}
	}
}

public class Mine
{
	string name;
	float cost;
	bool unlocked = false;
	bool active = false;
    float mineBonus = 0;

	public string Name { get { return name; } }

	public float Cost { get { return cost; } }

	public bool Unlocked { get { return unlocked; } set { unlocked = value; } }

	public bool Active { get { return active; } set { active = value; } }

    public float MineBonus { get { return mineBonus; } }

	/// <summary>
	/// Initializes a new instance of the <see cref="Mine"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="cost">Cost.</param>
	public Mine(string name, float cost, float mineBonus)
	{
		this.name = name;
		this.cost = cost;
        this.mineBonus = mineBonus;
	}
}
