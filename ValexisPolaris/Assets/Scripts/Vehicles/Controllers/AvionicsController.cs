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
        SetupThrusterGroups();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupThrusterGroups()
    {
        foreach (Transform thr in _thrusterRoot.transform)
        {
            var axes = ThrusterUtils.GetAxes(thr);
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
