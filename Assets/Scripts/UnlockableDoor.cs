using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnlockableDoor : MonoBehaviour {

    public Keypad MyKeypad;

    protected Animator _anim;

	// Use this for initialization
	void Start ()
    {
        _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(MyKeypad.ValidEnteredCode)
        {
            _anim.SetBool("IsOpen", true);
            MyKeypad.SetVisible(false);
        }
        else
        {
            _anim.SetBool("IsOpen", false);
            MyKeypad.SetVisible(true);
        }
	}
}
