using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour {

    public int[] skillDurations;
    public Image[] cooldownImages;
    public int baseMultiplier;
    public int skillMultiplier;
    public float stepDivider;
    bool skillOneActive = false;

    private int multiplier;
    private int stepBonus = 0;

    public PickaxeUpgrade pickaxe;


    void Start()
    {
        multiplier = baseMultiplier;
    }

    public void clickPickaxe()
    {
        pickaxe.IncreaseCurrency(multiplier);
    }

    public void CalculateStepBonus()
    {
        float round = (GlobalItems.stepCount / stepDivider);
        stepBonus = (int)(round + 0.5f);
    }

    public void SkillOne()
    {
        CalculateStepBonus();
        multiplier = skillMultiplier + stepBonus;
        if (!skillOneActive)
        {
            skillOneActive = true;
            StartCoroutine(ImageSkillCooldown(skillDurations[0], cooldownImages[0]));
            Invoke("EndOfSkill", skillDurations[0]);
        }
    }

    public void EndOfSkill()
    {
        multiplier = baseMultiplier;
        skillOneActive = false;
    }

    IEnumerator ImageSkillCooldown(float time, Image image)
    {
        image.fillAmount = 1;
        while (image.fillAmount > 0.0f)
        {
            image.fillAmount -= Time.deltaTime / time;
            yield return null;
        }
    }

    public void SkillTwo()
    {

    }

    public void SkillThree()
    {

    }

    public void SkillFour()
    {

    }

    public void SkillFive()
    {

    }

    public void SkillSix()
    {

    }

}
