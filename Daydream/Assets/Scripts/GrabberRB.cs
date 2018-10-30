using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberRB : MonoBehaviour
{
    public GvrPointerPhysicsRaycaster playerPointerRaycaster;
    public Transform distanceCheckTransform;

    //Moving object to held position variables
    public float LiftStrength = 3.0f;
    public float MaxReachDistance = 3.0f;
    public float HoldDistance = 1.5f;
    public float AutoReleaseRadius = 0.5f;
    protected bool _hasReachedHoldPosition = false;

    //General variables
    protected bool _holding = false;
    protected Rigidbody _grabbedObjectRigidBody = null;
    protected Collider _grabbedObjectCollider = null;

    //Rotating object with touchpad swipes variables
    protected GvrControllerInputDevice _gvrControllerDevice;
    protected Vector2 _previousTouchPos;
    protected Vector3 _highestAngularVelocity = Vector3.zero;
    protected bool _wasTouching = false;
    public float swipeMultiplier = 10.0f;

    private void Awake()
    {
        if(!playerPointerRaycaster)
        {
            Debug.LogError(name + " does not have playerPointerRaycaster assigned!");
        }
    }

    protected virtual void FixedUpdate ()
    {   
		if(_holding)
        {
            if(!_grabbedObjectRigidBody)
            {
                Release();
                return;
            }

            #region Move held object to position
            //Check to see if rigidbody should be dropped
            if (!CanBeHeldByPlayer())
            {
                //Drop _grabbedObject
                Release();
            }
            else
            {
                GvrBasePointer.PointerRay pointerRay = playerPointerRaycaster.GetLastRay();
                Ray heldPositionRay = new Ray(transform.position, pointerRay.ray.direction);
                Vector3 holdPosition = heldPositionRay.GetPoint(HoldDistance);

                //Move _grabbedObject to holdPosition
                Vector3 liftVector = (holdPosition - _grabbedObjectRigidBody.transform.position) / Time.fixedDeltaTime;
                if (liftVector.sqrMagnitude > LiftStrength * LiftStrength)
                {
                    liftVector = liftVector.normalized * LiftStrength;
                }

                _grabbedObjectRigidBody.velocity = liftVector;
            }
            #endregion

            #region Rotate held object based on swiping
            if (_gvrControllerDevice == null)
            {
                Debug.Log("Looking for controller input device...");
                _gvrControllerDevice = GvrControllerInput.GetDevice(GvrControllerHand.Dominant);
            }
            else if (_gvrControllerDevice.GetButton(GvrControllerButton.TouchPadTouch))
            {
                Vector2 currentTouchPos = _gvrControllerDevice.TouchPos;

                if (_wasTouching)
                {
                    Vector2 deltaTouch = currentTouchPos - _previousTouchPos;

                    Vector3 heldObjectAngularVelocity = transform.right * deltaTouch.y;
                    heldObjectAngularVelocity += Vector3.up * -1 * deltaTouch.x;
                    heldObjectAngularVelocity *= swipeMultiplier / Time.fixedDeltaTime;
                    //new Vector3(0.0f, -1 * deltaTouch.x, 0.0f) * swipeMultiplier;
                    _grabbedObjectRigidBody.angularVelocity = heldObjectAngularVelocity;
                    if(heldObjectAngularVelocity.sqrMagnitude > _highestAngularVelocity.sqrMagnitude)
                    {
                        _highestAngularVelocity = heldObjectAngularVelocity;
                    }
                    _highestAngularVelocity *= 0.7f;
                }

                _wasTouching = true;
                _previousTouchPos = currentTouchPos;
            }
            else if (_wasTouching)
            {
                
                _grabbedObjectRigidBody.angularVelocity = _highestAngularVelocity;
                _highestAngularVelocity = Vector3.zero;

                _wasTouching = false;
            }
            #endregion
        }
    }

    protected bool CanBeHeldByPlayer()
    {
        //Leaving this here because I may want to reimpliment it later.
        /*Collider[] foundCols = Physics.OverlapSphere(holdPosition, AutoReleaseRadius);
        
        foreach(Collider c in foundCols)
        {
            if (c == _grabbedObjectCollider)
            {
                return true;
            }
        }

        return false;*/

        if(!_grabbedObjectRigidBody) { return false; }

        float heldObjectDistanceSquared = (distanceCheckTransform.transform.position - _grabbedObjectRigidBody.position).sqrMagnitude;
        return  heldObjectDistanceSquared < MaxReachDistance * MaxReachDistance;
    }

    #region Grab & Release
    public void Grab()
    {
        if (_holding) { return; }

        GvrBasePointer.PointerRay pointerRay = playerPointerRaycaster.GetLastRay();
        RaycastHit hit;
        if(Physics.Raycast(pointerRay.ray, out hit, MaxReachDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            if(hit.rigidbody)
            {
                _grabbedObjectRigidBody = hit.rigidbody;
                _grabbedObjectCollider = hit.collider;

                if(CanBeHeldByPlayer())
                {
                    _holding = true;
                }
                else
                {
                    _grabbedObjectCollider = null;
                    _grabbedObjectRigidBody = null;
                }
            }
        }
    }

    public void Release()
    {
        _holding = false;

        _grabbedObjectCollider = null;
        _grabbedObjectRigidBody = null;
    }

    public void ToggleGrab()
    {
        if(_holding)
        {
            Release();
        }
        else
        {
            Grab();
        }
    }

    private void OnDisable()
    {
        Release();
    }
    #endregion
}
