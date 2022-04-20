using System;
using UnityEngine;

public abstract class Tool : Interactable
{
    [SerializeField] protected float xPositionHold;
    [SerializeField] protected float yPositionHold;
    [SerializeField] protected float xRotationHold;
    [SerializeField] protected float yRotationHold;
    [SerializeField] protected float zRotationHold;

    public void IsBeingHeldBy(Transform heldBy)
    {
        shouldShowInfo = false;
        
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        
        transform.parent = heldBy;
        transform.localPosition = Vector3.forward * xPositionHold + Vector3.up * yPositionHold;
        transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(xRotationHold, yRotationHold, zRotationHold), 1);
    }

    public abstract void PerformAction();

    public void IsDropped()
    {
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        
        transform.parent = null;

    }
}