using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Screen;

public class PlayerController : MonoBehaviour
{
    public Transform playerBody;
    public CharacterController controller;

    private float _xRotation = 0f;
    private Tool _mToolHeld;
    private float mouseSensitivity = 200f;
    private Camera _mGameCamera;
    private const float Speed = 10f;
    private Vector3 _screenCenter;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _mGameCamera = GetComponent<Camera>();
        _screenCenter = new Vector3(width / 2, height / 2);
    }

    void Update()
    {
        UpdateViewDirection();
        UpdatePosition();

        if (Physics.Raycast(_mGameCamera.ScreenPointToRay(_screenCenter), out var hit))
        {
            UpdateRaycast(hit);
        }
    }

    private void UpdateRaycast(RaycastHit hit)
    {
        var interactable = hit.collider.GetComponentInParent<Interactable>();
        if (interactable != null)
        {
            interactable.UpdateInformationPanel(_mGameCamera);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            PerformAction(hit);
        }
    }

    private void PerformAction(RaycastHit hit)
    {
        switch (hit.transform.tag)
        {
            case "Tool":
                PickUpTool(hit.collider.GetComponentInParent<Tool>());
                break;
            case "Garden":
                UseTool();
                break;
            case "Terrain":
                DropToolInPossession();
                break;
        }
    }

    private void UseTool()
    {
        if (_mToolHeld == null) return;
        
        _mToolHeld.PerformAction();
    }

    private void PickUpTool(Tool newTool)
    {
        DropToolInPossession();
        _mToolHeld = newTool;
        newTool.IsBeingHeldBy(playerBody);
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
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void UpdatePosition()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = playerBody.right * x + playerBody.forward * z;

        controller.Move(move * (Speed * Time.deltaTime));
    }
}