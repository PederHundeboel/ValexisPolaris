using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle
{
    public int something = 1;
    public Wheel[] wheels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Wheel w in wheels)
        {
            var target = w.GetMountPoint();
            w.gameObject.transform.RotateAround(target.transform.position, Vector3.forward, 20 * Time.deltaTime);
            Debug.Log("spinnin");
        }
    }
}
