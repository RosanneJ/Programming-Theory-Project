using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        float x = 5 * Input.GetAxis("Mouse X");
        float y = 5 * Input.GetAxis("Mouse Y");
        transform.Rotate(x, y, 0);
    }
}
