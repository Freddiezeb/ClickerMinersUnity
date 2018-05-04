using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSteps : MonoBehaviour {

    AndroidJavaClass androidClass;
    public Text displaySteps;

    public void getSteps(string message)
    {
        displaySteps.text = "Steps: " + message;
        GlobalItems.stepCount = int.Parse(message);
    }
}
