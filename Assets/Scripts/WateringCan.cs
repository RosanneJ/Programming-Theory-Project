using System;
using System.Collections;
using UnityEngine;

public class WateringCan : Tool
{
    private Quaternion _endRotation;
    private Vector3 _endPosition;

    public float tippingSpeed = 8;

    private float _smashProgress = -1;
    private float _resetProgress = -1;
    private float _zRotation = 55;

    private void Start()
    {
        // Processes
        _smashProgress = -1;
        _resetProgress = -1;
    }

    private void Update()
    {
        // For smooth rotation, we process this every frame
        ProcessRotation();
    }

    public override void PerformAction(RaycastHit hit)
    {
        // calculate end rotation and position
        float calculatedZPosition = hit.transform.position.z - transform.position.z - 2.5f;

        _endPosition = new Vector3(hit.transform.position.x, hit.transform.position.y + 1,
            calculatedZPosition);
        _endRotation = Quaternion.Euler(RotationHeld.x,RotationHeld.y, _zRotation);

        // kicks of the rotation process per frame (in Update > ProcessRotation)
        _smashProgress = 0;
    }

    private void ProcessRotation()
    {
        // rotation is started when _smashProgress is set to zero
        if (_smashProgress < 1 && _smashProgress >= 0)
        {
            _smashProgress += Time.deltaTime * tippingSpeed;
            transform.localRotation = Quaternion.Lerp(RotationHeld, _endRotation, _smashProgress);
            //transform.position = Vector3.Lerp(PositionHeld, _endPosition, _smashProgress);
        }
        else if (_resetProgress < 1 && _resetProgress >= 0)
        {
            _resetProgress += Time.deltaTime * tippingSpeed;
            transform.localRotation = Quaternion.Lerp(_endRotation, RotationHeld, _resetProgress);
            //transform.position = Vector3.Lerp(_endPosition, PositionHeld, _resetProgress);
        }

        if (_smashProgress >= 1)
        {
            _smashProgress = -1;
            _resetProgress = 0;
        }

        if (_resetProgress >= 1)
        {
            _resetProgress = -1;
        }
    }
}