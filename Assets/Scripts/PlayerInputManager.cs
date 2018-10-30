using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    public TeleportScript teleporter;
    public GrabberRB grabber;
    public MenuManager menu;

    public float grabPressTimeOut = 0.25f;

    protected GvrControllerInputDevice _gvrControllerDevice;

    protected float _buttonDownTime;
    protected bool _buttonIsDown = false;

    protected enum InputAction
    {
        NONE,
        GRAB,
        TELEPORT
    }
    protected InputAction _currentAction = InputAction.NONE;

    protected void Update()
    {
        //Main click
        if (_buttonIsDown)
        {
            if (Time.timeSinceLevelLoad > _buttonDownTime + grabPressTimeOut)
            {
                if(_currentAction != InputAction.TELEPORT)
                {
                    _currentAction = InputAction.TELEPORT;
                    if (teleporter)
                    {
                        teleporter.OnActivate();
                    }
                }
            }
        }

        //App button
        if (_gvrControllerDevice == null)
        {
            _gvrControllerDevice = GvrControllerInput.GetDevice(GvrControllerHand.Dominant);
        }
        else
        {
            if (_gvrControllerDevice.GetButtonDown(GvrControllerButton.App))
            {
                menu.OpenMenu();
            }
        }
    }

    public void OnDown()
    {
        _buttonDownTime = Time.timeSinceLevelLoad;
        _buttonIsDown = true;
        _currentAction = InputAction.GRAB;
    }

    public void OnUP()
    {
        _buttonIsDown = false;

        if(grabber && _currentAction == InputAction.GRAB)
        {
            grabber.ToggleGrab();
        }
        if(teleporter && _currentAction == InputAction.TELEPORT)
        {
            teleporter.OnDeactivate();
        }

        _currentAction = InputAction.NONE;
    }
}
