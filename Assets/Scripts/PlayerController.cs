using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    public float Speed;
    
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateViewDirection();
        UpdatePosition();
    }

    private void UpdateViewDirection()
    {
        float y = 5 * Input.GetAxis("Mouse X");
        transform.Rotate(0, y, 0);
    }

    private void UpdatePosition()
    {
        if (Input.GetKeyDown(PlayerControl.Forward))
        {
            m_Rigidbody.velocity = transform.forward * Speed;
        } else if (Input.GetKeyDown(PlayerControl.Backward))
        {
            m_Rigidbody.velocity = -transform.forward * Speed;
        }

        if (Input.GetKeyDown(PlayerControl.Left))
        {
            m_Rigidbody.velocity = -transform.right * Speed;
        } else if (Input.GetKeyDown(PlayerControl.Right))
        {
            m_Rigidbody.velocity = transform.right * Speed;
        }
    }
}
