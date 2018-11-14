using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    public TeleportScript teleporter;
    public GrabberRB grabber;
    public MenuManager menu;

    protected bool _triggerActivelyHeld = false;

    protected void Update()
    {
        //Main trigger
        if (!_triggerActivelyHeld && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            _triggerActivelyHeld = true;

            if (teleporter)
            {
                teleporter.OnActivate();
            }
        }
        else if(_triggerActivelyHeld)
        {
            teleporter.OnDeactivate();
            _triggerActivelyHeld = false;
        }

        if(OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
        {
            grabber.ToggleGrab();
        }

        //App button
        if (OVRInput.Get(OVRInput.Button.Back))
        {
            menu.OpenMenu();
        }
    }
}
