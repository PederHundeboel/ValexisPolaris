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
    private bool _isThruster = false;
    [SerializeField]
    private ThrustDir _thrustDir = ThrustDir.NoDir;

    private InputMaster _controls;
    private Rigidbody _rb;
    void Awake()
    {
        _tx = transform.localPosition.x;
        _tz = transform.localPosition.z;
        _yrot = transform.localRotation.y;

        _controls = new InputMaster();
        _controls.Rocket.Enable();
        if (!_isThruster)
        {
            _controls.Rocket.Thrust.performed += _ => _on = true;
            _controls.Rocket.Thrust.canceled += _ => _on = false;
        }
        else
        {
            _controls.Rocket.Roll.performed += DoRoll;
            _controls.Rocket.Roll.canceled += _ => _on = false;
            _thrustDir = GetThrustDirFromTransform(transform);

        }
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

    private void LateUpdate()
    {
        
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

    enum ThrustDir
    {
        PositiveRoll,
        NegativeRoll,
        PositiveYaw,
        NegativeYaw,
        PositivePitch,
        NegativePitch,
        NoDir
    }

    private ThrustDir GetThrustDirFromTransform(Transform t)
    {
        var tx = t.localPosition.x;
        var tz = t.localPosition.y;
        var yrot = t.localEulerAngles.x;

        Debug.Log(transform.forward);

        //Positive roll
        //x+ z-
        if ((tx >= 0 && tz <= 0) && (yrot >= 0 && _yrot <= 90))
            return ThrustDir.PositiveRoll;
        //x- z-
        if ((tx <= 0 && tz <= 0) && (yrot > 90 && yrot <= 180))
            return ThrustDir.PositiveRoll;
        //x- z+
        if ((tx <= 0 && tz >= 0) && (yrot > 180 && yrot <= 270))
            return ThrustDir.PositiveRoll;
        //x+ z+
        if ((tx >= 0 && tz >= 0) && (yrot > 270 && yrot <= 359.999))
            return ThrustDir.PositiveRoll;

        //Negative roll
        //x+ z-
        if ((tx >= 0 && tz <= 0) && (yrot > 180 && yrot <= 270))
            return ThrustDir.NegativeRoll;
        //x- z-
        if ((tx <= 0 && tz <= 0) && (_yrot > 270 && yrot <= 359.999))
            return ThrustDir.NegativeRoll;
        //x- z+
        if ((tx <= 0 && tz >= 0) && (yrot >= 0 && yrot <= 90))
            return ThrustDir.NegativeRoll;
        //x+ z+
        if ((tx >= 0 && tz >= 0) && (yrot > 90 && yrot <= 180))
            return ThrustDir.NegativeRoll;

        return ThrustDir.NoDir;
    }
}
