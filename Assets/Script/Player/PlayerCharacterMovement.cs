using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacterMovement: MonoBehaviour
{
    [SerializeField]
    private float _currentSpeed = 1;
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private float _gravityScale = 1;
    private Vector3 _movementDirection;
    private Vector3 _velocityXZ;
    private float _velocityY;
    private bool _isGrounded;

    public void SetMoveDirection(Vector2 inputDirection)
    {
        _movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
    }

    public void Move()
    {
        CalculateVelocityXZ();
        CalculateVelocityY();
        Vector3 velocity = new Vector3(_velocityXZ.x,_velocityY,_velocityXZ.z);
        _characterController.Move(velocity * Time.deltaTime);
    }

    private void Update()
    {
        CheckIsGrounded();
        ResetVelocity();
        Move();
    }

    private void CheckIsGrounded()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        _isGrounded = Physics.CheckSphere(transform.position,0.5f, groundLayer);
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

    private void CalculateVelocityY()
    {
        _velocityY = _velocityY + Physics.gravity.y * _gravityScale * Time.deltaTime;
    }

    private void ResetVelocity()
    {
        if (_isGrounded == true && _velocityY < 0)
        {
            _velocityY = -2;
        }
    }
}
