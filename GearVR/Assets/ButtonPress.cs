using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public Text output;

    private int _pressCount = 0;

    private void Start()
    {
        UpdateText();
    }

    public void ButtonDown()
    {
        _pressCount++;
        UpdateText();
    }

    private void UpdateText()
    {
        if(!output) { return; }
        output.text = "Press count: " + _pressCount;
    }
}
