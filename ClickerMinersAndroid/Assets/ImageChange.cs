using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour {

    public Button imageBtn;
    public Sprite pressedSprite;
    public Sprite releasedSprite;

    public void onPress ()
    {
        imageBtn.GetComponent<Image>().sprite = pressedSprite;
    }

    public void onRelease()
    {
        imageBtn.GetComponent<Image>().sprite = releasedSprite;
    }
}
