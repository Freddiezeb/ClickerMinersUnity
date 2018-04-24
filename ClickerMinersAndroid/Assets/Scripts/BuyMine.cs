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

	Mine gravelMine;
	Mine graniteMine;
	Mine metalMine;
	Mine obsidianMine;
	Mine goldMine;

	List<Mine> mineList;

	void Start ()
	{
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
	}

    public void BuyGravelMine()
    {
		HandelMine(gravelMine);
    }

    public void BuyGraniteMine()
    {
		HandelMine(graniteMine);
    }

    public void BuyMetalMine()
    {
		HandelMine(metalMine);
    }

    public void BuyObsidianMine()
    {
		HandelMine(obsidianMine);
    }

    public void BuyGoldMine()
    {
		HandelMine(goldMine);
    }

	public void MinePayment(Mine mine)
	{
		if (GlobalClicks.currencyCount >= mine.Cost)
		{
			GlobalClicks.currencyCount -= mine.Cost;
			mine.Unlocked = true;
			mine.Active = true;
		}
		else
		{
			StartCoroutine(FadeTextToZeroAlpha(2f, textBox));
		}
	}

	void HandelMine(Mine mine)
	{
		UnActivateMine (mine);
		if (!mine.Unlocked) 
		{
			MinePayment (mine);
		} 
		else if(mine.Unlocked) 
		{
			if (mine.Active)
			{
				mine.Active = false;
			} else 
			{
				mine.Active = true;
			}
		}
		Debug.Log (mine.Name + " Active: " + mine.Active.ToString ());
	}

	void UnActivateMine(Mine currentMine)
	{
		bool isActive = currentMine.Active;
		for (int i = 0; i < mineList.Count; i++) 
		{
			mineList [i].Active = false;
		}
		currentMine.Active = isActive;
	}
		
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
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

		public Mine(string name, int cost)
		{
			this.name = name;
			this.cost = cost;
		}
	}
}

