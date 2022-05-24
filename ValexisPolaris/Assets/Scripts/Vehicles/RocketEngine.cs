using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketEngine : MonoBehaviour
{
    [SerializeField]
    protected int _maxThrust = 10;
    [SerializeField]
    protected int _minThrust = 1;
    [SerializeField]
    protected float _thrustPercent = 0.5f;
    // Start is called before the first frame update

    private float _tx;
    private float _tz;
    private float _yrot;

    protected bool _on = false;

    private ValexisInput _controls;
    protected Rigidbody _rb;
    void Awake()
    {
        _tx = transform.localPosition.x;
        _tz = transform.localPosition.z;
        _yrot = transform.localRotation.y;

        _controls = new ValexisInput();
        _controls.Rocket.Enable();
        //if (!_isThruster)
        //{
        _controls.Rocket.Thrust.performed += _ => _on = true;
        _controls.Rocket.Thrust.canceled += _ => _on = false;
        //}
        //else
        //{
        //    _thrustDir = GetThrustDirFromTransform(transform);
        //    if (_thrustDir == ThrustDir.PositiveRoll || _thrustDir == ThrustDir.NegativeRoll)
        //    {
        //        _controls.Rocket.Roll.performed += DoRoll;
        //        _controls.Rocket.Roll.canceled += _ => _on = false;
        //    }
        //    else
        //    {
        //        _controls.Rocket.Pitch.performed += DoPitch;
        //        _controls.Rocket.Pitch.canceled += _ => _on = false;
        //    }


        //}
        _rb = GetComponent<Rigidbody>();
    }
}
