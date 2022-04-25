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

    public abstract void PerformAction(RaycastHit hit);

    public void IsDropped()
    {
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        ShouldShowInfo = true;
        transform.parent = null;

    }
}