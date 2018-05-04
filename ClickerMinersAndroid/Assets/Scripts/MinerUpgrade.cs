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
    public float[] passiveBonus;

    public List<Text> minerTextList;
    public List<Text> researchTextList;
    public List<Miner> minerList;

    Canvas researchCanvas;

    TextFader textFader;

    public Button btnBuyMiner;
    public Button btnResearchUpgrade;

    void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Shows the canvas.
    /// </summary>
    /// <param name="show">If set to <c>true</c> show.</param>
    public void ShowMinerCanvas(bool show)
    {
        ShowCanvas(minerCanvas, show);
    }

    /// <summary>
    /// Shows the canvas.
    /// </summary>
    /// <param name="show">If set to <c>true</c> show.</param>
    public void ShowResearchCanvas(bool show)
    {
        ShowCanvas(researchCanvas, show);
    }

    void ShowCanvas(Canvas canvas, bool show)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(show);
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

        gravelMiner = new Miner("gravelMiner", minerCost[0], researchCosts[0], passiveBonus[0]);
        graniteMiner = new Miner("graniteMiner", minerCost[1], researchCosts[1], passiveBonus[1]);
        metalMiner = new Miner("metalMiner", minerCost[2], researchCosts[2], passiveBonus[2]);
        obsidianMiner = new Miner("obsidianMiner", minerCost[3], researchCosts[3], passiveBonus[3]);
        goldMiner = new Miner("goldMiner", minerCost[4], researchCosts[4], passiveBonus[4]);

        minerList = new List<Miner>();
        minerList.Add(gravelMiner);
        minerList.Add(graniteMiner);
        minerList.Add(metalMiner);
        minerList.Add(obsidianMiner);
        minerList.Add(goldMiner);

        temp = GameObject.Find("ResearchCanvas");
        if (temp != null)
        {
            researchCanvas = temp.GetComponent<Canvas>();
            researchCanvas.gameObject.SetActive(false);
            Debug.Log("ResearchCanvas is initialized");
        }
        else
        {
            Debug.Log("Could not find ResearchCanvas in Scene");
        }

        temp = GameObject.Find("MinerCanvas");
        if (temp != null)
        {
            minerCanvas = temp.GetComponent<Canvas>();
            minerCanvas.gameObject.SetActive(false);
            Debug.Log("MinerCanvas is initialized");
        }
        else
        {
            Debug.Log("Could not find MinerCanvas in Scene");
        }
    }

    public void MinerPayment(Miner miner, Text displayText)
    {
        if (GlobalClicks.currencyCount >= miner.Cost)
        {
            GlobalClicks.currencyCount -= miner.Cost;
            miner.Unlocked = true;
            StopAllCoroutines();
            StartMinersPassive();
            minerTextList[0].text = "";
            minerTextList[1].text = "Owned";
            ShowCanvas(researchCanvas, true);
            SetResearchText(miner);
            displayText.text = "$/S: " + miner.PassiveBonus.ToString();
        }
        else
        {
            textFader.DisplayText(GlobalItems.displayTimes[1], GlobalItems.displayTexts[0]);
        }
    }

    private void StartMinersPassive()
    {
        if (gravelMiner.Unlocked)
        {
            StartCoroutine(PassiveGravelMiner());
        }
        if (graniteMiner.Unlocked)
        {
            StartCoroutine(PassiveGraniteMiner());
        }
        if (metalMiner.Unlocked)
        {
            StartCoroutine(PassiveMetalMiner());
        }
        if (obsidianMiner.Unlocked)
        {
            StartCoroutine(PassiveObsidianMiner());
        }
        if (goldMiner.Unlocked)
        {
            StartCoroutine(PassiveGoldMiner());
        }
    }

    public void ResearchUpgrade(Miner miner)
    {
        if (GlobalClicks.currencyCount >= miner.ResearchCost)
        {
            GlobalClicks.currencyCount -= miner.ResearchCost;
            miner.ResearchLevel++;
            IncreaseResearchCost(miner);
            IncreasePassiveBonus(miner);
            SetResearchText(miner);
        }
        else
        {
            textFader.DisplayText(GlobalItems.displayTimes[1], GlobalItems.displayTexts[0]);
        }
    }

    void IncreaseResearchCost(Miner miner)
    {
        miner.ResearchCost += GlobalItems.CalculateNewPrice(miner.ResearchStartCost, 1.15f, miner.ResearchLevel);
    }

    void IncreasePassiveBonus(Miner miner)
    {
        miner.PassiveBonus += GlobalItems.CalculateNewPrice(miner.PassiveBonus, 1.15f, miner.ResearchLevel);
    }

    public void SetResearchText(Miner miner)
    {
        researchTextList[0].text = "$" + miner.ResearchCost.ToString();
        researchTextList[1].text = miner.ResearchLevel.ToString();
    }

    IEnumerator PassiveGravelMiner()
    {
        while (true)
        {
            AddPassiveBonus(gravelMiner);
            yield return new WaitForSeconds(GlobalItems.passiveWaitTime);
        }
    }

    IEnumerator PassiveGraniteMiner()
    {
        while (true)
        {
            AddPassiveBonus(graniteMiner);
            yield return new WaitForSeconds(GlobalItems.passiveWaitTime);
        }
    }

    IEnumerator PassiveMetalMiner()
    {
        while (true)
        {
            AddPassiveBonus(metalMiner);
            yield return new WaitForSeconds(GlobalItems.passiveWaitTime);
        }
    }

    IEnumerator PassiveObsidianMiner()
    {
        while (true)
        {
            AddPassiveBonus(obsidianMiner);
            yield return new WaitForSeconds(GlobalItems.passiveWaitTime);
        }
    }

    IEnumerator PassiveGoldMiner()
    {
        while (true)
        {
            AddPassiveBonus(goldMiner);
            yield return new WaitForSeconds(GlobalItems.passiveWaitTime);
        }
    }

    public void AddPassiveBonus(Miner miner)
    {
        if (miner.Unlocked)
        {
            GlobalClicks.currencyCount += miner.PassiveBonus;
        }
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
    float passiveBonus;
    float passiveStartBonus;

    public string Name { get { return name; } set { name = value; } }

    public float Cost { get { return cost; } set { cost = value; } }

    public bool Unlocked { get { return unlocked; } set { unlocked = value; } }

    public int ResearchLevel { get { return researchLevel; } set { researchLevel = value; } }

    public float ResearchCost { get { return researchCost; } set { researchCost = value; } }

    public float PassiveBonus { get { return passiveBonus; } set { passiveBonus = value; } }

    public float ResearchStartCost { get { return researchStartCost; } }

    public float PassiveStartBonus { get { return passiveStartBonus; } }

    /// <summary>
    /// Initializes a new instance of the <see cref="Miner"/> class.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="cost">Cost.</param>
    public Miner(string name, float cost, float researchCost, float passiveBonus)
    {
        this.name = name;
        this.cost = cost;
        this.researchCost = researchCost;
        this.passiveBonus = passiveBonus;
        researchStartCost = researchCost;
        passiveStartBonus = passiveBonus;
        researchLevel = 1;
    }
}
