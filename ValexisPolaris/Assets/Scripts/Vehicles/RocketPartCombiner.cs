using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPartCombiner : MonoBehaviour
{
    public SphereCollider buildRoot;
    public Vector3 mountPoint;

    // Start is called before the first frame update
    void Start()
    {
        buildRoot =  gameObject.AddComponent<SphereCollider>();
        buildRoot.radius = 2;
        buildRoot.isTrigger = true;
        buildRoot.center = mountPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != transform && other.gameObject.GetComponent<RocketPart>() && !other.isTrigger)
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.position = transform.position + mountPoint - other.gameObject.GetComponent<RocketPart>().mountingPoint;
            //other.transform.rotation = transform.rotation;
            this.buildRoot.enabled = false;
            var joint = gameObject.AddComponent<FixedJoint>();
            var connectingRb = other.gameObject.GetComponent<Rigidbody>();
            Debug.Log(other.gameObject.ToString());
            other.transform.SetParent(transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
