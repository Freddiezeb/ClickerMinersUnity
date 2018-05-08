using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{

    public int[] skillDurations;
    public int[] skillCooldowns;
    public Image[] cooldownImages;
    public float baseMultiplier;
    public float skillMultiplier;
    public float stepDivider;
    bool skillOneActive = false;
    bool skillTwoActive = false;
    bool skillThreeActive = false;

    private float multiplier;
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
        if (!skillOneActive)
        {
            skillOneActive = true;
            CalculateStepBonus();
            multiplier = skillMultiplier + stepBonus;
            StartCoroutine(ImageSkillCooldown(skillDurations[0], cooldownImages[0]));
            Invoke("EndOfSkillOne", skillDurations[0]);
        }
    }

    public void EndOfSkillOne()
    {
        multiplier = baseMultiplier;
        StartCoroutine(ImageSkillCooldown(skillCooldowns[0], cooldownImages[1]));
        Invoke("EndOfCooldownOne", skillCooldowns[0]);
    }

    public void EndOfCooldownOne()
    {
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
        if (!skillTwoActive)
        {
            skillTwoActive = true;
            CalculateStepBonus();
            GlobalItems.minerPassiveMultiplier = 100 + stepBonus;
            StartCoroutine(ImageSkillCooldown(skillDurations[1], cooldownImages[2]));
            Invoke("EndOfSkill", skillDurations[1]);
        }
    }

    public void EndOfSkillTwo()
    {
        GlobalItems.minerPassiveMultiplier = baseMultiplier;
        StartCoroutine(ImageSkillCooldown(skillCooldowns[1], cooldownImages[3]));
        Invoke("EndOfCooldownTwo", skillCooldowns[1]);
    }

    public void EndOfCooldownTwo()
    {
        skillTwoActive = false;
    }

    public void SkillThree()
    {
        if (!skillThreeActive)
        {
            CalculateStepBonus();
            float decreasedTickTime = 0.065f * stepBonus;
            skillThreeActive = true;
            if (0.8f - decreasedTickTime >= 0.4f)
                GlobalItems.passiveWaitTime = 0.8f - decreasedTickTime;
            else
                GlobalItems.passiveWaitTime = 0.4f;

            StartCoroutine(ImageSkillCooldown(skillDurations[2], cooldownImages[4]));
            Invoke("EndOfSkillThree", skillDurations[2]);
        }
    }

    public void EndOfSkillThree()
    {
        GlobalItems.passiveWaitTime = baseMultiplier;
        StartCoroutine(ImageSkillCooldown(skillCooldowns[2], cooldownImages[5]));
        Invoke("EndOfCooldownThree", skillCooldowns[2]);
    }

    public void EndOfCooldownThree()
    {
        skillThreeActive = false;
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
