using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour {

    private bool isPressed = false;
    public Button imageBtn;
    public Sprite pressedSprite;
    public Sprite releasedSprite;

    void start()
    {
    }
    public void onPress ()
    {
        Debug.Log("Pressed");
        //imageBtn.image = pressedSprite;
        imageBtn.GetComponent<Image>().sprite = pressedSprite;
    }

    public void onRelease()
    {
        Debug.Log("Released");
        //imageBtn.image = releasedSprite;
        imageBtn.GetComponent<Image>().sprite = releasedSprite;
    }
}
