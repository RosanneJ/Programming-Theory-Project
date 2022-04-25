using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class WalkingGarden : Interactable
{
    public float MovingSpeed = 1f;

    public float DistanceBeforeTurn = 4f;

    private Rigidbody _rb;

    private bool _changeDirection = true;
    private Vector3 _previousPosition;
    private Quaternion _targetDirection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        // move je kontje
        _rb.velocity = transform.forward * MovingSpeed * Time.deltaTime;
        
        if (_changeDirection)
        {
            // save starting position
            _previousPosition = transform.position;
            
            // turn object in random direction
            float randomDirection = Random.Range(0, 360);
            _targetDirection = Quaternion.Euler(0, randomDirection, 0);
            transform.rotation = _targetDirection;
        }

        // check if distance before turn is reached
        _changeDirection = Vector3.Distance(_previousPosition, transform.position) >= DistanceBeforeTurn;
    }
}
