using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerBody;
    public CharacterController controller;
    
    private float xRotation = 0f;
    private Tool _mToolHeld;
    private float mouseSensitivity = 200f;
    private Camera _mGameCamera;
    private const float Speed = 10f;
    private Vector3 _screenCenter;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _mGameCamera = GetComponent<Camera>();
        _screenCenter = new Vector3(x: Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {
        UpdateViewDirection();
        UpdatePosition();
        
        var ray = _mGameCamera.ScreenPointToRay(_screenCenter);
        if (Physics.Raycast(ray, out var hit))
        { 
            if (hit.transform.CompareTag("Tool"))
            {
                Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
                if (interactable.closeEnough)
                {
                    interactable.ShowInfo(_mGameCamera);
                }
                else
                {
                    interactable.HideInfo();
                }
            }
        }

        if (_mToolHeld != null)
        {
            ToolHandling();
        }

    }

    private void ToolHandling()
    {
        var ray = _mGameCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        { 
            if (hit.transform.CompareTag("Terrain"))
            {
                DropTool();
                _mToolHeld = null;
            } else if (hit.transform.CompareTag("Garden"))
            {
                _mToolHeld.PerformAction();
            }
        }
    }
    
    private void TakeTool()
    {
        var ray = _mGameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Tool"))
            {
                //the collider could be children of the unit, so we make sure to check in the parent
                var tool = hit.collider.GetComponentInParent<Tool>();
                _mToolHeld = tool;
                AttachToPlayer(tool);
            }

        }
    }

    private void DropTool()
    {
        _mToolHeld.transform.parent = null;
        _mToolHeld.ResetPosition();
        _mToolHeld = null;
    }

    private void AttachToPlayer(Tool tool)
    {
        tool.transform.parent = playerBody;
        tool.transform.position = new Vector3(0, 0, 0);
    }

    private void UpdateViewDirection()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void UpdatePosition()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = playerBody.right * x + playerBody.forward * z;

        controller.Move(move * Speed * Time.deltaTime);
    }
}
