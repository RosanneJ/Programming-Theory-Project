using UnityEngine;
using Random = UnityEngine.Random;

public class WalkingGarden : Interactable
{
    [SerializeField] private float movingSpeed = 1f;
    [SerializeField] private float distanceBeforeTurn = 4f;

    private bool _changeDirection = true;
    private Vector3 _previousPosition;
    private Quaternion _targetDirection;

    new void Update()
    {
        MoveForwards();
        
        if (_changeDirection)
        {
            _previousPosition = transform.position;
            ChangeDirection();
        }

        _changeDirection = ShouldChangeDirection();

        base.Update();
    }

    private bool ShouldChangeDirection()
    {
        var walkingDistanceReached = Vector3.Distance(_previousPosition, transform.position) >= distanceBeforeTurn;
        return walkingDistanceReached || CloseEnough;
    }

    private void ChangeDirection()
    {
        float randomDirection = Random.Range(0, 360);
        _targetDirection = Quaternion.Euler(0, randomDirection, 0);
        transform.rotation = _targetDirection;
    }

    private void MoveForwards()
    {
        Rb.velocity = transform.forward * movingSpeed * Time.deltaTime;
    }
}
