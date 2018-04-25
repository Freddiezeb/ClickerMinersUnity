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

	public int gravelMinerCost;
	public int graniteMinerCost;
	public int metalMinerCost;
	public int obsidianMinerCost;
	public int goldMinerCost;

	Mine gravelMine;
	Mine graniteMine;
	Mine metalMine;
	Mine obsidianMine;
	Mine goldMine;

	List<Mine> mineList;
	List<int> minerCostList;

	MinersUpgrade minerUpgrade;

	SpriteRenderer spriteRenderer;

	public Sprite[] stoneSprites;

	void Start ()
	{
		Initialize ();
	}

    public void BuyGravelMine()
    {
		HandleMine(gravelMine, 1);
    }

    public void BuyGraniteMine()
    {
		HandleMine(graniteMine, 2);
    }

    public void BuyMetalMine()
    {
		HandleMine(metalMine, 0);
    }

    public void BuyObsidianMine()
    {
		HandleMine(obsidianMine, 0);
    }

    public void BuyGoldMine()
    {
		HandleMine(goldMine, 0);
    }

	/// <summary>
	/// The payment method for the mine.
	/// </summary>
	/// <param name="mine">Mine.</param>
	public void MinePayment(Mine mine, int spriteIndex)
	{
		if (GlobalClicks.currencyCount >= mine.Cost)
		{
			GlobalClicks.currencyCount -= mine.Cost;
			mine.Unlocked = true;
			mine.Active = true;
			minerUpgrade.ShowCanvas (true);
			setStoneSprite (spriteIndex);
		}
		else
		{
			StartCoroutine(FadeTextToZeroAlpha(2f, textBox));
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
				minerUpgrade.ShowCanvas (false);
				setStoneSprite (0);

			} else 
			{
				mine.Active = true;
				minerUpgrade.ShowCanvas (true);
				setStoneSprite (spriteIndex);
			}
		}
		Debug.Log (mine.Name + " Active: " + mine.Active.ToString ());
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
	/// Fades the text to zero alpha.
	/// </summary>
	/// <returns>The text to zero alpha.</returns>
	/// <param name="t">T.</param>
	/// <param name="i">The index.</param>
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

	/// <summary>
	/// Sets the stone sprite.
	/// </summary>
	/// <param name="spriteIndex">Sprite index.</param>
	void setStoneSprite (int spriteIndex) {
		spriteRenderer.sprite = stoneSprites [spriteIndex];
	}

	/// <summary>
	/// Initialize resources.
	/// </summary>
	void Initialize(){
		gravelMine = new Mine ("gravelMine", gravelMineCost);
		graniteMine = new Mine ("graniteMine", graniteMineCost);
		metalMine = new Mine ("metalMine", metalMineCost);
		obsidianMine = new Mine ("obsidianMine", obsidianMineCost);
		goldMine = new Mine ("goldMine", goldMineCost);

		mineList = new List<Mine>();
		mineList.Add (gravelMine);
		mineList.Add (graniteMine);
		mineList.Add (metalMine);
		mineList.Add (obsidianMine);
		mineList.Add (goldMine);

		minerCostList = new List<int> ();
		minerCostList.Add (gravelMinerCost);
		minerCostList.Add (graniteMinerCost);
		minerCostList.Add (metalMinerCost);
		minerCostList.Add (obsidianMinerCost);
		minerCostList.Add (goldMinerCost);

		minerUpgrade = new MinersUpgrade (minerCostList);

		GameObject temp = GameObject.Find ("stone");
		if (temp != null) {
			spriteRenderer = temp.GetComponent<SpriteRenderer> ();
			setStoneSprite (0);
			Debug.Log ("spriteRenderer is initialized");
		} else {
			Debug.Log ("Could not find stone in Scene");
		}
	}
}

public class Mine
{
	string name;
	int cost;
	bool unlocked = false;
	bool active = false;

	public string Name { get { return name; } }

	public int Cost { get { return cost; } }

	public bool Unlocked { get { return unlocked; } set { unlocked = value; } }

	public bool Active { get { return active; } set { active = value; } }

	/// <summary>
	/// Initializes a new instance of the <see cref="Mine"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="cost">Cost.</param>
	public Mine(string name, int cost)
	{
		this.name = name;
		this.cost = cost;
	}
}