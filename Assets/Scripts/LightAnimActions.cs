using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimActions : MonoBehaviour {

    public bool CanInteract = false;
    public Animator FadeCanvasAnim;
    public MenuManager mm;

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
    }
}
