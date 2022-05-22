using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketEngine : MonoBehaviour
{
    [SerializeField]
    private int _maxThrust = 10;
    [SerializeField]
    private int _minThrust = 1;
    [SerializeField]
    private float _thrustPercent = 0.5f;
    // Start is called before the first frame update

    private float _tx;
    private float _tz;
    private float _yrot;

    private bool _on = false;

    [SerializeField]
    private ThrustDir _thrustDir = ThrustDir.NoDir;

    private ValexisInput _controls;
    private Rigidbody _rb;
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

    private void OnDisable()
    {
        _controls.Rocket.Roll.performed -= DoRoll;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_on)
        {
            //Debug.Log("Thrusters on!");
            _rb.AddForce(transform.forward * GetThrust(), ForceMode.Force);
        }
    }

    public float GetThrust()
    {
        return _thrustPercent*_maxThrust;
    }

    private void DoRoll(InputAction.CallbackContext c)
    {
        if (c.ReadValue<float>() == 1)
        {
            if (_thrustDir == ThrustDir.PositiveRoll)
            {
                Debug.Log("Positive roll! from:" + transform.ToString());
                _on = true;
            }
        }
        else
        {
            if (_thrustDir == ThrustDir.NegativeRoll)
            {
                Debug.Log("Negative Roll! from: " + transform.ToString());
                _on = true;
            }
        }
    }

    private void DoPitch(InputAction.CallbackContext c)
    {
        if (c.ReadValue<float>() == 1)
        {
            if (_thrustDir == ThrustDir.PositivePitch)
            {
                Debug.Log("Positive Pitch! from:" + transform.ToString());
                _on = true;
            }
        }
        else
        {
            if (_thrustDir == ThrustDir.NegativePitch)
            {
                Debug.Log("Negative Pitch! from: " + transform.ToString());
                _on = true;
            }
        }
    }

    enum ThrustDir
    {
        PositiveRoll,
        NegativeRoll,
        PositiveYaw,
        NegativeYaw,
        PositivePitch,
        NegativePitch,
        Fwd,
        NoDir
    }

    private ThrustDir GetThrustDirFromTransform(Transform t)
    {
        var tx = t.localPosition.x;
        var ty = t.localPosition.y;
        var tz = t.localPosition.z;
        var thrustDir = (Quaternion.Euler(t.parent.eulerAngles) * t.transform.forward);
        var yrot = t.localEulerAngles.x;

        Debug.Log(gameObject.name + transform.localPosition + "\n tr local fwd = " + (t.parent.parent.parent.localRotation * t.transform.forward));

        if (ThrustRollDir(tx, ty, tz, thrustDir) != ThrustDir.Fwd)
            return ThrustRollDir(tx, ty, tz, thrustDir);

        return ThrustDir.NoDir;
    }

    private ThrustDir ThrustRollDir(float tx, float ty, float tz, Vector3 thrustDir)
    {
        if (thrustDir.y == 0 && thrustDir.x == 0)
            return ThrustDir.Fwd;

        if (Mathf.Abs(thrustDir.y) + Mathf.Abs(thrustDir.x) > Mathf.Abs(thrustDir.z))
        {
            if ((tx >= 0 && ty >= 0))
            {
                if ((thrustDir.x <= 0 && thrustDir.y >= 0))
                    return ThrustDir.PositiveRoll;
                if ((thrustDir.x >= 0 && thrustDir.y <= 0))
                    return ThrustDir.NegativeRoll;
            }
            if ((tx <= 0 && ty >= 0))
            {
                if ((thrustDir.x <= 0 && thrustDir.y <= 0))
                    return ThrustDir.PositiveRoll;
                if ((thrustDir.x >= 0 && thrustDir.y >= 0))
                    return ThrustDir.NegativeRoll;
            }
            if ((tx <= 0 && ty <= 0))
            {
                if ((thrustDir.x >= 0 && thrustDir.y <= 0))
                    return ThrustDir.PositiveRoll;
                if ((thrustDir.x <= 0 && thrustDir.y >= 0))
                    return ThrustDir.NegativeRoll;
            }
            if ((tx >= 0 && ty <= 0))
            {
                if ((thrustDir.x >= 0 && thrustDir.y >= 0))
                    return ThrustDir.PositiveRoll;
                if ((thrustDir.x <= 0 && thrustDir.y <= 0))
                    return ThrustDir.NegativeRoll;
            }
        }

        //Positive pitch (only picks up thrusters above COG atm)
        if ((ty < 0 && tz > 0 && thrustDir.z > 0) || (ty > 0 && tz > 0 && thrustDir.z < 0))
            return ThrustDir.PositivePitch;
        //Negative pitch (only picks up thrusters above COG atm)
        if ((ty < 0 && tz > 0 && thrustDir.z < 0) || (tx > 0 && tz > 0 && thrustDir.z > 0))
            return ThrustDir.NegativePitch;

        return ThrustDir.NoDir;

        

    }
}
