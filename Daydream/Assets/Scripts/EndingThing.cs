using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingThing : MonoBehaviour {

    public Animator coolThing;

    public MeshRenderer BallRenderer;
    public Material BallHoverMat;
    private Material _ballBaseMat;
    

    public LightAnimActions ei;

    private void Start()
    {
        _ballBaseMat = BallRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Special")
        {
            coolThing.SetTrigger("StartCoolThing");
            Destroy(other.gameObject);
        }
    }
    
    public void BallHoveredEnter()
    {
        if (!ei.CanInteract) { return; }

        BallRenderer.material = BallHoverMat;
    }

    public void BallHoveredExit()
    {
        if (!ei.CanInteract) { return; }

        BallRenderer.material = _ballBaseMat;
    }

    public void BallClicked()
    {
        if (!ei.CanInteract) { return; }

        coolThing.SetTrigger("EndCoolThing");
    }
}
