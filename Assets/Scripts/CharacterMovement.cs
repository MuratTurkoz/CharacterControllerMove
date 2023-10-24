using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _rotationSpeed = 6f;
    [SerializeField] private float _jumpPower = 5;
    private float _gravity = -9.81f;
    [SerializeField] private bool _isGrounded;
    private CharacterController _characterController;
    private Vector3 _jumpDirection;


    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1.1f))
        {
            if (hit.collider.name == "Plane")
            {
                _jumpDirection.y = 0;
                _isGrounded = true;
            }
        }

        //Move
        Vector3 moveDirection = transform.TransformDirection(Vector3.forward);
        float currentMoveSpeed = Input.GetAxis("Vertical") * _moveSpeed;
        _characterController.Move(moveDirection * currentMoveSpeed * Time.deltaTime);

        //Rotate

        transform.Rotate(0, Input.GetAxis("Horizontal") * _rotationSpeed, 0);

        //Jump

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isGrounded = false;
            _jumpDirection.y += Mathf.Sqrt(_jumpPower * _gravity * -3.0f);
        }
        _jumpDirection.y += _gravity * Time.deltaTime;
        _characterController.Move(_jumpDirection * Time.deltaTime);



    }
}
