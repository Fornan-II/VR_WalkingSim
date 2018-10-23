using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {

    public Text OutputField;

    public string passcode = "0451";
    public int passcodeLength = 4;

    protected bool _validEnteredCode = false;
    public bool ValidEnteredCode { get { return _validEnteredCode; } }

    public void EnterValue(string value)
    {
        Debug.Log("EnterValue(" + value + ")");
        if (value == "" && (OutputField.text.Length >= 1))
        {
            Debug.Log("Backspace");
            OutputField.text = OutputField.text.Remove(OutputField.text.Length - 1);
        }

        if (OutputField.text.Length >= passcodeLength)
        {
            return;
        }

        OutputField.text += value;
    }

    public void CheckEnteredCode()
    {
        if(OutputField.text != passcode)
        {
            _validEnteredCode = false;
            return;
        }

        Debug.Log("Value matches.");
        _validEnteredCode = true;
    }

    public void SetVisible(bool value)
    {
        if(value)
        {

        }
        else
        {

        }
    }
}
