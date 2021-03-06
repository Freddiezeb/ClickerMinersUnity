using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalItems {

    public static float passiveWaitTime = 1;

    public static int stepCount = 0;

    public static float minerPassiveMultiplier = 1;

    public static int[] displayTimes = displayTimes = new int[]{
            1,
            2,
            3,
            4,
            5
        };

    public static string[] displayTexts = displayTexts = new string[]{
            "YOU DONT HAVE ENOUGH $",
            "THIS IS A COOL FEATURE",
            "WELCOME TO OUR COOL GAME"
        };
		
	public static float CalculateNewPrice(float baseCost, float multiplier, int level)
	{
		return (float)(baseCost * (float)Math.Pow(multiplier, level - 1.0f));
	}
}
