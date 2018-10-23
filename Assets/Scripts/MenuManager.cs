using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RectTransform))]
public class MenuManager : MonoBehaviour {

    public bool StartActive = false;
    public GameObject menu;
    public Transform playerCameraTransform;

    protected RectTransform _rt;
    protected GvrControllerInputDevice _gvrControllerDevice;

    private void Start()
    {
        menu.SetActive(StartActive);

        _rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_gvrControllerDevice == null)
        {
            _gvrControllerDevice = GvrControllerInput.GetDevice(GvrControllerHand.Dominant);
        }
        else
        {
            if (_gvrControllerDevice.GetButtonDown(GvrControllerButton.App))
            {
                OpenMenu();
            }
        }
    }

    public void ToggleMenu()
    {
        if(menu.activeSelf)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    public void OpenMenu()
    {
        Vector3 newRotation = playerCameraTransform.forward;
        newRotation.y = 0.0f;
        _rt.forward = newRotation;
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void ReloadScene()
    {
        Debug.Log("Does nothing currently because of conflicts with daydream core functionality.");
        //SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
    }

    public void QuitApplication()
    {
        Debug.Log("Quitting application");
        Application.Quit();
    }
}
