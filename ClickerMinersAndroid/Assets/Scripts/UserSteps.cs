using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSteps : MonoBehaviour {

    AndroidJavaClass androidClass;
    public Text displaySteps;
	// Use this for initialization
	void Start () {
		
	}

    public void getSteps(string message)
    {
        displaySteps.text = "Steps: " + message;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
