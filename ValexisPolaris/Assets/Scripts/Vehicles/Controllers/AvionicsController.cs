using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AvionicsController : MonoBehaviour
{

    [SerializeField]
    private List<RcsThruster> _positiveRoll = new List<RcsThruster>();
    [SerializeField]
    private List<RcsThruster> _negativeRoll = new List<RcsThruster>();
    [SerializeField]
    private List<RcsThruster> _positivePitch = new List<RcsThruster>();
    [SerializeField]
    private List<RcsThruster> _negativePitch = new List<RcsThruster>();
    [SerializeField]
    private List<RcsThruster> _positiveYaw = new List<RcsThruster>();
    [SerializeField]
    private List<RcsThruster> _negativeYaw = new List<RcsThruster>();
    [SerializeField]
    private GameObject _thrusterRoot;
    private ValexisInput _controls;

    private int _roll = 0;
    private int _pitch = 0;
    private int _yaw = 0;

    private void Awake()
    {
        var rocketBody = GameObjectHelper.FindChildWithTag(transform.gameObject, "RocketBody");
        _thrusterRoot = GameObjectHelper.FindChildWithTag(rocketBody, "ThrusterGroup");
        SetupThrusterGroups();
        _controls = new ValexisInput();
        _controls.Rocket.Enable();
        _controls.Rocket.Roll.performed += Rolling;
        _controls.Rocket.Pitch.performed += Pitching;
        _controls.Rocket.Yaw.performed += Yawing;
        _controls.Rocket.Roll.canceled += _ => _roll = 0;
        _controls.Rocket.Pitch.canceled += _ => _pitch = 0;
        _controls.Rocket.Yaw.canceled += _ => _yaw = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_roll > 0)
        {
            foreach (var thr in _positiveRoll)
            {
                thr.Thrust();
            }
        } else if (_roll < 0)
        {
            foreach (var thr in _negativeRoll)
            {
                thr.Thrust();
            }
        }
        else if (_pitch > 0)
        {
            foreach (var thr in _positivePitch)
            {
                thr.Thrust();
                Debug.Log("Pitching pos");
            }
        }
        else if (_pitch < 0)
        {
            foreach (var thr in _negativePitch)
            {
                thr.Thrust();
                Debug.Log("Pitching neg");
            }
        }
        else if (_yaw > 0)
        {
            foreach (var thr in _positiveYaw)
            {
                thr.Thrust();
                Debug.Log("Yawing pos");
            }
        }
        else if (_yaw < 0)
        {
            foreach (var thr in _negativeYaw)
            {
                thr.Thrust();
                Debug.Log("Yawing neg");
            }
        }
    }

    private void Rolling(InputAction.CallbackContext c)
    {
        _roll = (int)c.ReadValue<float>();
    }

    private void Pitching(InputAction.CallbackContext c)
    {
        _pitch = (int)c.ReadValue<float>();
    }

    private void Yawing(InputAction.CallbackContext c)
    {
        _yaw = (int)c.ReadValue<float>(); 
    }

    private void SetupThrusterGroups()
    {
        foreach (Transform thr in _thrusterRoot.transform)
        {
            var axes = ThrusterUtils.GetAxes(thr, _thrusterRoot.transform, this.transform);
            foreach (var axis in axes)
            {
                switch (axis)
                {
                    case ThrusterUtils.ThrustAxis.PositiveRoll:
                        _positiveRoll.Add(thr.gameObject.GetComponent<RcsThruster>());
                        break;
                    case ThrusterUtils.ThrustAxis.NegativeRoll:
                        _negativeRoll.Add(thr.gameObject.GetComponent<RcsThruster>());
                        break;
                    case ThrusterUtils.ThrustAxis.PositivePitch:
                        _positivePitch.Add(thr.gameObject.GetComponent<RcsThruster>());
                        break;
                    case ThrusterUtils.ThrustAxis.NegativePitch:
                        _negativePitch.Add(thr.gameObject.GetComponent<RcsThruster>());
                        break;
                    case ThrusterUtils.ThrustAxis.PositiveYaw:
                        _positiveYaw.Add(thr.gameObject.GetComponent<RcsThruster>());
                        break;
                    case ThrusterUtils.ThrustAxis.NegativeYaw:
                        _negativeYaw.Add(thr.gameObject.GetComponent<RcsThruster>());
                        break;
                }

            }

        }
    }
}
