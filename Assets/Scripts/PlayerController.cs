using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera _mGameCamera;
    private Rigidbody _mRigidbody;

    private Tool _mToolHeld;

    private const float Speed = 5f;

    void Start()
    {
        _mRigidbody = GetComponent<Rigidbody>();
        _mGameCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        UpdateViewDirection();
        UpdatePosition();

        if (Input.GetMouseButtonUp(0))
        {
            UpdateMouseClick();   
        }
    }
    
    private void UpdateMouseClick()
    {
        if (_mToolHeld != null)
        {
            ToolHandling();
        }
        else
        {
            TakeTool();
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
        Destroy(_mToolHeld.gameObject.GetComponent<FixedJoint>());
        _mToolHeld.ResetPosition();
        _mToolHeld = null;
    }

    private void AttachToPlayer(Tool tool)
    {
        var playerTransform = transform;
        var localPosition = playerTransform.localPosition;

        tool.transform.position = new Vector3(localPosition.x -2, localPosition.y + 2, localPosition.z + 3);
        tool.transform.Rotate(0, 90, 0);
        tool.gameObject.AddComponent<FixedJoint>();
        tool.GetComponent<FixedJoint>().connectedBody = _mRigidbody;

    }

    private void UpdateViewDirection()
    {
        float y = 5 * Input.GetAxis("Mouse X");
        transform.Rotate(0, y, 0);
    }

    private void UpdatePosition()
    {
        if (Input.GetKey(PlayerControl.Forward))
        {
            _mRigidbody.velocity = transform.forward * Speed;
        } else if (Input.GetKey(PlayerControl.Backward))
        {
            _mRigidbody.velocity = -transform.forward * Speed;
        }

        if (Input.GetKey(PlayerControl.Left))
        {
            _mRigidbody.velocity = -transform.right * Speed;
        } else if (Input.GetKey(PlayerControl.Right))
        {
            _mRigidbody.velocity = transform.right * Speed;
        }
    }
}
