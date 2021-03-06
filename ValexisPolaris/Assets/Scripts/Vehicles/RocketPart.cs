using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPart : MonoBehaviour
{
    public GameObject MountPoint;
    public Vector3 mountPoint;
    public Vector3 mountingPoint;
    public SphereCollider connectCollider;
    public Rigidbody rb;
    public List<FixedJoint> joints = new List<FixedJoint>();
    // Start is called before the first frame update
    void Start()
    {
        connectCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != transform && other.gameObject.GetComponent<RocketPart>() && !other.isTrigger)
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = rb.velocity;
            other.transform.position = transform.position + mountPoint - other.gameObject.GetComponent<RocketPart>().mountingPoint;
            other.transform.rotation = transform.rotation;
            this.connectCollider.enabled = false;
            var joint = gameObject.AddComponent<FixedJoint>();
            var connectingRb = other.gameObject.GetComponent<Rigidbody>();
            Debug.Log(other.gameObject.ToString());
            Debug.Log(connectingRb.useGravity);
            other.transform.SetParent(transform.parent);
            joint.connectedBody = connectingRb;
            joints.Add(joint);
        }
            
    }

    private void ConnectPart()
    {
        
    }
}
