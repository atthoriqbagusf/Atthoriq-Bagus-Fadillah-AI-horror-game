using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacterMovement: MonoBehaviour
{
    [SerializeField]
    private float _currentSpeed = 1;
    [SerializeField]
    private CharacterController _characterController;
    private Vector3 _movementDirection;
    private Vector3 _velocityXZ;

    public void SetMoveDirection(Vector2 inputDirection)
    {
        _movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
    }

    public void Move()
    {
        if (_movementDirection.magnitude > 0.01)
        {
        _velocityXZ = _movementDirection * _currentSpeed;
        }
        else
        {
            _velocityXZ = Vector3.zero;
        }
        _characterController.Move(_velocityXZ * Time.deltaTime);
    }


    private void Update()
    {
        Move();
    }
}
