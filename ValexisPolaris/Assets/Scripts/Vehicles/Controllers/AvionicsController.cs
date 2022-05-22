using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        var rocketBody = GameObjectHelper.FindChildWithTag(transform.gameObject, "RocketBody");
        _thrusterRoot = GameObjectHelper.FindChildWithTag(rocketBody, "ThrusterGroup");

        foreach (Transform thr in _thrusterRoot.transform)
        {
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
