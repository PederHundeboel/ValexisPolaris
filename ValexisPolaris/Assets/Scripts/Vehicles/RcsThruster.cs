using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RcsThruster : RocketEngine
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(gameObject.name + transform.localPosition + "\n tr local fwd = " + (transform.InverseTransformDirection(transform.transform.forward)));
        //Debug.Log(gameObject.name + transform.localPosition + "\n tr local fwd = " + transform.forward + "VS. : " + (Quaternion.Euler(transform.localEulerAngles) * transform.InverseTransformDirection(transform.forward)) + "VS. : " + (transform.InverseTransformDirection(Vector3.forward)) + "world ");
        Debug.Log(gameObject.name + transform.localPosition + "\n tr local fwd = " + (Quaternion.Euler(transform.localEulerAngles) * transform.InverseTransformDirection(transform.forward)));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
