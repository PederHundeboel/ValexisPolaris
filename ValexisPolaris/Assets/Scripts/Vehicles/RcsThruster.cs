using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RcsThruster : RocketEngine
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name + transform.localPosition + "\n tr local fwd = " + (Quaternion.Euler(transform.localEulerAngles) * transform.InverseTransformDirection(transform.forward)));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
