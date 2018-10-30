using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEndScript : MonoBehaviour {

    public Animator FadeCanvasAnim;
    public UnlockableDoor trackedEndingDoor;
    public float FadeToWhiteDelay = 7.0f;
    public UnityEngine.UI.Text MenuPanelText;

    private bool doorOpenTriggered = false;
	
	// Update is called once per frame
	void Update ()
    {
		if(trackedEndingDoor.IsOpen && !doorOpenTriggered)
        {
            StartCoroutine(FadeToWhite());
            doorOpenTriggered = true;
        }
	}

    private IEnumerator FadeToWhite()
    {
        for(float elapsed = 0.0f; elapsed < FadeToWhiteDelay; elapsed += Time.deltaTime)
        {
            yield return null;
        }

        FadeCanvasAnim.SetTrigger("Fade");
        MenuPanelText.alignment = TextAnchor.MiddleCenter;
        MenuPanelText.fontSize = 29;
        MenuPanelText.color = new Color(0, 191, 183);
        MenuPanelText.text = "The End";
    }
}
