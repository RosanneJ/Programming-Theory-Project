using UnityEngine;

public class Interactable : InformationUnit
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    // ENCAPSULATION
    protected Rigidbody Rb { get; private set; }
    protected bool IsGrounded { get; set; }
    
    private Vector3 _velocity;

    protected new void Awake()
    {
        base.Awake();
        Rb = GetComponentInChildren<Rigidbody>();
    }

    // ABSTRACTION
    protected void Update()
    {
        UpdateGravity();
    }

    private void UpdateGravity()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

        if (IsGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += GameManager.Gravity * Time.deltaTime;
        Rb.AddForce(Vector3.up * _velocity.y, ForceMode.Acceleration);
    }
}