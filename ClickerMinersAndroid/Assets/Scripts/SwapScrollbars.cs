using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapScrollbars : MonoBehaviour {

    public GameObject[] goScrollbars;
    public Image swapButtonImage;
    public Text swapButtonText;
    public Sprite[] sprites;

    public GameObject minerCanvas;

    public void SwapScrollbar()
    {
        if(goScrollbars[0].activeSelf)
        {
            swapButtonImage.sprite = sprites[0];
            swapButtonText.text = "MINES";
            goScrollbars[0].SetActive(false);
            goScrollbars[1].SetActive(true);
            minerCanvas.SetActive(false);
        }
     else
        {
            swapButtonImage.sprite = sprites[1];
            swapButtonText.text = "SKILLS";
            goScrollbars[0].SetActive(true);
            goScrollbars[1].SetActive(false);
            minerCanvas.SetActive(true);
        }
    }
}
