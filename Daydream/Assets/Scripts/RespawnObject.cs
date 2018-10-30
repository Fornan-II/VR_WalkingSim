using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour {

    protected static float RespawnYLevel = -64.0f;

    protected Vector3 _startingPosition;
    protected Quaternion _startingRotation;

	void Awake ()
    {
        _startingPosition = transform.position;
        _startingRotation = transform.rotation;
	}
	
	void FixedUpdate ()
    {
		if(transform.position.y < RespawnYLevel)
        {
            Respawn();
        }
	}

    public void Respawn()
    {
        transform.position = _startingPosition;
        transform.rotation = _startingRotation;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if(rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
