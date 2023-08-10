using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float jumpForce = 5f;
    [SerializeField] 
    private float speed = 5f;
    
    private Rigidbody _rb;
    private bool _isGrounded;
    private ValexisInput _controls;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controls = new ValexisInput();
        _controls.Player.Enable();
    }

    private void FixedUpdate()
    {
        var input = _controls.Player.Move.ReadValue<Vector2>();

        var moveVec = new Vector3(input.x, 0, input.y);
        
        if (input.magnitude != 0)
        {
            _rb.AddForce(moveVec*speed);            
        }

        if (_controls.Player.Jump.WasPressedThisFrame() && _isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("YUMP");
            _isGrounded = false;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}
