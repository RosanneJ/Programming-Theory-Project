using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private  Transform groundCheck;
    [SerializeField] private  Camera playerCamera;

    [SerializeField] private  float groundDistance;
    [SerializeField] private  LayerMask groundMask;

    private float _xRotation = 0f;
    private Tool _mToolHeld;
    private float mouseSensitivity = 200f;
    private const float Speed = 10f;
    private bool _isGrounded;

    private Vector3 _velocity;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // ABSTRACTION
    void Update()
    {
        UpdateViewDirection();
        UpdatePosition();
        UpdateGravity();

        if (Physics.Raycast(playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out var hit))
        {
            UpdateRaycast(hit);
        }
    }

    private void UpdateGravity()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += GameManager.Gravity * Time.deltaTime;

        controller.Move(_velocity * Time.deltaTime);

    }

    private void UpdateRaycast(RaycastHit hit)
    {
        var interactable = hit.collider.GetComponentInParent<Interactable>();
        if (interactable != null)
        {
            interactable.UpdateInformationPanel(playerCamera);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            PerformAction(hit);
        }
        
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            UseTool(hit);
        }
    }

    // ABSTRACTION
    private void PerformAction(RaycastHit hit)
    {
        switch (hit.transform.tag)
        {
            case "Tool":
                PickUpTool(hit.collider.GetComponentInParent<Tool>());
                break;
            case "Garden":
                UseTool(hit);
                break;
            default:
                DropToolInPossession();
                break;
        }
    }

    // ABSTRACTION
    private void UseTool(RaycastHit hit)
    {
        if (_mToolHeld == null) return;
        
        _mToolHeld.PerformAction();
    }

    // ABSTRACTION
    private void PickUpTool(Tool newTool)
    {
        DropToolInPossession();
        _mToolHeld = newTool;
        newTool.IsBeingHeldBy(transform);
    }

    private void DropToolInPossession()
    {
        if (_mToolHeld == null) return;

        _mToolHeld.IsDropped();
        _mToolHeld = null;
    }

    private void UpdateViewDirection()
    {
        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -70f, 70f);
        
        playerCamera.gameObject.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void UpdatePosition()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = transform.right * x + transform.forward * z;

        controller.Move(move * (Speed * Time.deltaTime));
    }
}