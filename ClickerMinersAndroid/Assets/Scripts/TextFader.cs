using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour {

    [SerializeField]
    Text textBox;
	
    public void DisplayText(float displayTime, string displayText)
    {
        textBox.text = displayText;
        StartCoroutine(FadeTextToZeroAlpha(displayTime, textBox));	
    }

    /// <summary>
    /// Fades the text to zero alpha.
    /// </summary>
    /// <returns>The text to zero alpha.</returns>
    /// <param name="t">T.</param>
    /// <param name="i">The index.</param>
    IEnumerator FadeTextToZeroAlpha(float t, Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
