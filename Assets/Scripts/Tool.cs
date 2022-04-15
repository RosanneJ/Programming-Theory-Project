using System;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    private Quaternion _startRotation;
    public void Awake()
    {
        _startRotation = transform.rotation;
    }

    public void ResetPosition()
    {
        transform.rotation = _startRotation;
    }

    public abstract void PerformAction();
}