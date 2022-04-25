using System;
using UnityEngine;

public abstract class Tool : Interactable
{
    [SerializeField] protected float xPositionHold;
    [SerializeField] protected float yPositionHold;
    [SerializeField] protected float zPositionHold;
    [SerializeField] protected float xRotationHold;
    [SerializeField] protected float yRotationHold;
    [SerializeField] protected float zRotationHold;
    [SerializeField] protected  float zRotationTipping = 55;
    [SerializeField] protected float tippingSpeed = 8;
    
    private Quaternion _endRotation;
    private Vector3 _endPosition;

    private float _smashProgress = -1;
    private float _resetProgress = -1;
    

    protected Vector3 PositionHeld;
    protected Quaternion RotationHeld;

    public void IsBeingHeldBy(Transform heldBy)
    {
        ShouldShowInfo = false;
        HideInfo();
        
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        
        transform.parent = heldBy;
        
        PositionHeld = Vector3.forward * xPositionHold + Vector3.up * yPositionHold + Vector3.left * zPositionHold;
        RotationHeld = Quaternion.Lerp(transform.rotation, Quaternion.Euler(xRotationHold, yRotationHold, zRotationHold), 1);

        transform.localPosition = PositionHeld;
        transform.localRotation = RotationHeld;
    }

    private new void Start()
    {
        // Processes
        _smashProgress = -1;
        _resetProgress = -1;
        base.Start();
    }

    private new void Update()
    {
        // For smooth rotation, we process this every frame
        ProcessRotation();
        base.Update();
    }

    public void PerformAction(RaycastHit hit)
    {
        // calculate end rotation and position
        float calculatedZPosition = hit.transform.position.z - transform.position.z - 2.5f;

        _endPosition = new Vector3(hit.transform.position.x, hit.transform.position.y + 1,
            calculatedZPosition);
        _endRotation = Quaternion.Euler(RotationHeld.x,RotationHeld.y, zRotationTipping);

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

    public void IsDropped()
    {
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        ShouldShowInfo = true;
        transform.parent = null;

    }
}