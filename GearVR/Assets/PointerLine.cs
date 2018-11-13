using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(OVRGazePointer))]
public class PointerLine : MonoBehaviour
{
    private OVRGazePointer _myPointer;
    private LineRenderer _myLine;

    private void Start()
    {
        _myPointer = GetComponent<OVRGazePointer>();
        _myLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(_myPointer.hidden)
        {
            _myLine.enabled = false;
        }
        else
        {
            _myLine.enabled = true;
            Vector3[] lrPositions = new Vector3[] { _myPointer.rayTransform.position, _myPointer.gazeIcon.position };
            _myLine.SetPositions(lrPositions);
        }
    }
}
