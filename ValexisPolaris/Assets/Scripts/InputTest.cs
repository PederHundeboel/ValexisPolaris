using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private ValexisInput _controls;

    private float _x = 0;
    private float _y = 0;
    private float _z = 0;
    // Start is called before the first frame update
    void Start()
    {
        // draw a 5-unit white line from the origin for 2.5 seconds
        //Debug.DrawLine(transform.position, new Vector3(5, 0, 0) + transform.position, Color.green, 10.5f);
        //Debug.Log("DREW LINE");
        _controls = new ValexisInput();
        _controls.Rocket.Enable();

        _controls.Rocket.Roll.canceled += _ => _x = 0;
        _controls.Rocket.Pitch.canceled += _ => _z = 0;
        _controls.Rocket.Yaw.canceled += _ => _y = 0;
        _controls.Rocket.Roll.performed += Roll;
        _controls.Rocket.Pitch.performed += Pitch;
        _controls.Rocket.Yaw.performed += Yaw;
    }

    private void Roll(InputAction.CallbackContext c)
    {
        _x = c.ReadValue<float>();
    }

    private void Pitch(InputAction.CallbackContext c)
    {
        _z = c.ReadValue<float>();
    }

    private void Yaw(InputAction.CallbackContext c)
    {
        _y = c.ReadValue<float>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, new Vector3(_x, _y, _z).normalized + transform.position, Color.red);
    }
}
