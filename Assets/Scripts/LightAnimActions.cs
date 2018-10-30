using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimActions : MonoBehaviour {

    public bool CanInteract = false;
    public Animator FadeCanvasAnim;
    public MenuManager mm;
    public Transform PlayerTransform;
    public Vector3 EndGameTeleportCoordinate = new Vector3(0.0f, 100.0f, 0.0f);

    public void EnableInteract()
    {
        CanInteract = true;
    }

    public void TriggerFade()
    {
        FadeCanvasAnim.SetTrigger("Fade");
    }

    public void OpenMenu()
    {
        mm.OpenMenu();

        //Move player to somewhere where obstacles won't accidentally get in the way of the menu.
        if(PlayerTransform)
        {
            PlayerTransform.position = EndGameTeleportCoordinate;
        }
    }
}
