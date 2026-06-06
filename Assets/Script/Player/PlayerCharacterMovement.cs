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
        CalculateVelocityXZ();
        _characterController.Move(_velocityXZ * Time.deltaTime);
    }


    private void Update()
    {
        Move();
    }

    private void CalculateVelocityXZ()
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 xDirection = _movementDirection.x * cameraTransform.right;
        Vector3 zDirection = _movementDirection.z * cameraTransform.forward;
        Vector3 direction = xDirection + zDirection;
        direction.y = 0;
        if (_movementDirection.magnitude > 0.01)
        {
            _velocityXZ = direction.normalized * _currentSpeed;
        }
        else
        {
            _velocityXZ = Vector3.zero;
        }
    }
}
