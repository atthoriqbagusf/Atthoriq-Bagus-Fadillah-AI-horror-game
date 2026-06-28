using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacterMovement: MonoBehaviour
{
    [SerializeField]
    private float _sprintSpeed = 2;
    [SerializeField]
    private float _acceleration = 0.5f;
    [SerializeField]
    private float _walkSpeed = 1;
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private float _gravityScale = 1;
    private Vector3 _movementDirection;
    private Vector3 _velocityXZ;
    private float _velocityY;
    private bool _isGrounded;
    private bool _isSprint;
    private float _currentSpeed = 1;

    public bool IsSprint => _isSprint;
    public bool Enabled {get; private set; } = true;

    public void SetEnabled(bool isEnabled)
    {
        Enabled = isEnabled;
        if (_isSprint == true)
        {
            
        }
    }


    public void SetMoveDirection(Vector2 inputDirection)
    {
        _movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
    }

    public void SetSprint(bool isSprint)
    {
        _isSprint = isSprint;
    }

    public void Move()
    {
        if (Enabled == true)
        {
        CalculateVelocityXZ();
        CalculateVelocityY();
        Vector3 velocity = new Vector3(_velocityXZ.x,_velocityY,_velocityXZ.z);
        _characterController.Move(velocity * Time.deltaTime); 
        }
    }

    void Awake()
    {
        _currentSpeed = _walkSpeed;
    }

    private void Update()
    {
        CheckIsGrounded();
        CalculateAcceleration();
        ResetVelocity();
        Move();
    }

    private void CheckIsGrounded()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        _isGrounded = Physics.CheckSphere(transform.position,0.5f, groundLayer);
    }

    private void CalculateAcceleration()
    {
        if (_movementDirection.magnitude > 0.01)
        {
            if (_isSprint)
            {
                _currentSpeed = _currentSpeed + _acceleration * Time.deltaTime;
            }
            else
            {
                _currentSpeed = _currentSpeed - _acceleration * Time.deltaTime;
            }
            _currentSpeed = Mathf.Clamp(_currentSpeed,_walkSpeed,_sprintSpeed);
        }
        else
        {
            _currentSpeed = 0;
        }
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
