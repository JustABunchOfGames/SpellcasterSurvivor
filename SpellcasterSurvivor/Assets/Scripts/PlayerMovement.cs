using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _initialGravity = 1;
    [SerializeField] private float _gravity;
    
    [SerializeField] private bool _isGrounded;

    private Vector3 _camRotation;
    private Transform _cam;
    [SerializeField] private Vector3 _moveDirection;

    [Range(-90, -15)]
    [SerializeField] private int minAngle = -90;
    [Range(15, 90)]
    [SerializeField] private int maxAngle = 90;
    [Range(50, 500)]
    [SerializeField] private int sensitivity = 200;

    private void Awake()
    {
        _cam = Camera.main.transform;
        _gravity = _initialGravity;
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Mouse X"));

        _camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        _camRotation.x = Mathf.Clamp(_camRotation.x, minAngle, maxAngle);

        _cam.localEulerAngles = _camRotation;
    }

    private void Move()
    {
        _isGrounded = _characterController.isGrounded;

        // Move on horizontal axis
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move);
        _characterController.Move(move * _speed * Time.deltaTime);

        // Maintain grvaity on ground
        if (_isGrounded)
        {
            // Reset gravity once on ground (if it was modified earlier)
            if (_gravity != _initialGravity)
                _gravity = _initialGravity;

            // Reset height to not reduce infinitely
            if (_moveDirection.y < 0)
                _moveDirection.y = 0;
        }

        // Jump (if not flying)
        if (Input.GetButtonDown("Jump") && _initialGravity == _gravity && _moveDirection.y <= 0 && _moveDirection.y >= -0.5f)
        {
            _moveDirection.y += _jumpForce;
        }

        // Gravity + Execute Jump
        _moveDirection.y -= _gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    public void Fly(float jumpforce, float gravity)
    {
        _gravity = gravity;
        _moveDirection.y = 0;
        _moveDirection.y += _jumpForce;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}