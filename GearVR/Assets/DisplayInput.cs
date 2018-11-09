using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayInput : MonoBehaviour
{
    Text _outputField;

    private void Start()
    {
        _outputField = GetComponent<Text>();
    }

    private void Update ()
    {
        _outputField.text = GetOutput();
	}

    private string GetOutput()
    {
        OVRInput.Controller c = OVRInput.GetActiveController();
        string message = "Connected: " + c;

        message += "\nTouchPos: ";
        if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
        {
            Vector2 touchPos = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            message += "(" + touchPos.x + "," + touchPos.y + ")";
        }
        else
        {
            message += "N/A";
        }

        message +=
            "\nClick: " + OVRInput.Get(OVRInput.Button.PrimaryTouchpad) +
            "\nApp: " + OVRInput.Get(OVRInput.Button.Back) +
            "\nTrigger: " + OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);

        return message;
    }
}
