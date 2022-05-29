using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RcsThruster : RocketEngine
{

    //THIS IS HIGHLY TEMPORARY, DELETE AFTER DEBUGGING PLS :)
    public bool isPositive = true;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        //Debug.Log(gameObject.name + transform.localPosition + "\n tr local fwd = " + (Quaternion.Euler(transform.localEulerAngles) * transform.InverseTransformDirection(transform.forward)));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Thrust()
    {
        //_rb.AddForce(transform.forward * GetThrust(), ForceMode.Force);
        _rb.AddRelativeForce(transform.forward * GetThrust(), ForceMode.Force);
    }



    public float GetThrust()
    {
        return _thrustPercent * _maxThrust;
    }

    private void FixedUpdate()
    {
        if (_on)
        {
            //Debug.Log("Thrusters on!");
            //_rb.AddForce(transform.forward * GetThrust(), ForceMode.Force);
        }
    }
}
