using UnityEngine;

// INHERITANCE
public abstract class Tool : Interactable
{
    [SerializeField] private float xPositionHold;
    [SerializeField] private float yPositionHold;
    [SerializeField] private float zPositionHold;
    [SerializeField] private float xRotationHold;
    [SerializeField] private float xRotationTipping;
    [SerializeField] private float tippingSpeed;
    
    private Quaternion _endRotation;

    private float _smashProgress = -1;
    private float _resetProgress = -1;

    private Vector3 _positionHeld;
    private Quaternion _rotationHeld;

    private void Start()
    {
        _smashProgress = -1;
        _resetProgress = -1;
    }

    // POLYMORPHISM
    // ABSTRACTION
    private new void Update()
    {
        base.Update();
        ProcessSmashing();
        CheckProgressSmashing();
    }
    
    public void IsDropped()
    {
        Rb.useGravity = true;
        Rb.isKinematic = false;
        ShouldShowInfo = true;
        transform.parent = null;
        IsGrounded = false;
    }

    // ABSTRACTION
    public void IsBeingHeldBy(Transform heldBy)
    {
        ShouldShowInfo = false;
        IsGrounded = true;
        HideInfo();

        Rb.useGravity = false;
        Rb.isKinematic = true;

        transform.parent = heldBy;

        UpdatePositionHeld();
        UpdateRotationHeld();
    }
    
    // ABSTRACTION
    public void PerformAction()
    {
        _endRotation = Quaternion.Euler(xRotationTipping,_rotationHeld.y, _rotationHeld.z);
        
        StartSmashing();
    }

    private void CheckProgressSmashing()
    {
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
    
    private void UpdateRotationHeld()
    {
        var targetRotation = Quaternion.Euler(xRotationHold, 0, 0);
        _rotationHeld = Quaternion.Lerp(transform.rotation, targetRotation, 1);
        transform.localRotation = targetRotation;
    }

    private void UpdatePositionHeld()
    {
        _positionHeld = new Vector3(xPositionHold, yPositionHold, zPositionHold);
        transform.localPosition = _positionHeld;
    }

    private void StartSmashing()
    {
        _smashProgress = 0;
    }

    // ABSTRACTION
    private void ProcessSmashing()
    {
        if (_smashProgress < 1 && _smashProgress >= 0)
        {
            SmashRotation();
        }
        else if (_resetProgress < 1 && _resetProgress >= 0)
        {
            RotateBack();
        }
    }

    private void RotateBack()
    {
        _resetProgress += Time.deltaTime * tippingSpeed;
        transform.localRotation = Quaternion.Lerp(_endRotation, _rotationHeld, _resetProgress);
    }

    private void SmashRotation()
    {
        _smashProgress += Time.deltaTime * tippingSpeed;
        transform.localRotation = Quaternion.Lerp(_rotationHeld, _endRotation, _smashProgress);
    }
}