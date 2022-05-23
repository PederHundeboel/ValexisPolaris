using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterUtils : MonoBehaviour
{
    //The workings of this class might be slightly hard to understand fully without looking at the visualization found at: 

    static public List<ThrustAxis> GetAxes(Transform t)
    {
        var thr = new ThrusterTransform(t);
        var AxesList = new List<ThrustAxis>();
        

        if (thr.IsRoll())
            AxesList.Add(RollAxis(thr));
        if (thr.IsPitch())
            AxesList.Add(PitchAxis(thr));
        if(thr.IsYaw())
            AxesList.Add(YawAxis(thr));

        return AxesList;
    }

    static private ThrustAxis RollAxis(ThrusterTransform thr)
    {
        //1st quadrant
        if (thr.xPos >= 0 && thr.yPos <= 0)
        {
            if (thr.thrustDir.x <= 0 && thr.thrustDir.y <= 0)
                return ThrustAxis.PositiveRoll;
        }
        //2nd quadrant
        if (thr.xPos <= 0 && thr.yPos <= 0)
        {
            if (thr.thrustDir.x <= 0 && thr.thrustDir.y >= 0)
                return ThrustAxis.PositiveRoll;
        }
        //3rd quadrant
        if (thr.xPos <= 0 && thr.yPos >= 0)
        {
            if (thr.thrustDir.x >= 0 && thr.thrustDir.y >= 0)
                return ThrustAxis.PositiveRoll;
        }
        //4th quadrant
        if (thr.xPos >= 0 && thr.yPos >= 0)
        {
            if (thr.thrustDir.x >= 0 && thr.thrustDir.y <= 0)
                return ThrustAxis.PositiveRoll;
        }
        return ThrustAxis.NegativePitch;
    }

    static private ThrustAxis PitchAxis(ThrusterTransform thr)
    {
        //1st quadrant
        if (thr.yPos >= 0 && thr.zPos >= 0)
        {
            if (thr.thrustDir.y <= 0 && thr.thrustDir.z >= 0)
                return ThrustAxis.PositivePitch;
        }
        //2nd quadrant
        if (thr.yPos <= 0 && thr.zPos >= 0)
        {
            if (thr.thrustDir.y <= 0 && thr.thrustDir.z <= 0)
                return ThrustAxis.PositivePitch;
        }
        //3rd quadrant
        if (thr.yPos <= 0 && thr.zPos <= 0)
        {
            if (thr.thrustDir.y >= 0 && thr.thrustDir.z <= 0)
                return ThrustAxis.PositivePitch;
        }
        //4th quadrant
        if (thr.yPos >= 0 && thr.zPos <= 0)
        {
            if (thr.thrustDir.y >= 0 && thr.thrustDir.z >= 0)
                return ThrustAxis.PositivePitch;
        }
        return ThrustAxis.NegativePitch;
    }

    static private ThrustAxis YawAxis(ThrusterTransform thr)
    {
        //1st quadrant
        if (thr.xPos >= 0 && thr.zPos >= 0)
        {
            if (thr.thrustDir.x <= 0 && thr.thrustDir.z >= 0)
                return ThrustAxis.PositivePitch;
        }
        //2nd quadrant
        if (thr.xPos <= 0 && thr.zPos >= 0)
        {
            if (thr.thrustDir.x <= 0 && thr.thrustDir.z <= 0)
                return ThrustAxis.PositivePitch;
        }
        //3rd quadrant
        if (thr.xPos <= 0 && thr.zPos <= 0)
        {
            if (thr.thrustDir.x >= 0 && thr.thrustDir.z <= 0)
                return ThrustAxis.PositivePitch;
        }
        //4th quadrant
        if (thr.xPos >= 0 && thr.zPos <= 0)
        {
            if (thr.thrustDir.x >= 0 && thr.thrustDir.z >= 0)
                return ThrustAxis.PositivePitch;
        }
        return ThrustAxis.NegativePitch;
    }

    internal class ThrusterTransform
    {
        public float xPos;
        public float yPos;
        public float zPos;
        public Vector3 thrustDir;
        public ThrusterTransform(Transform t)
        {
            this.xPos = t.localPosition.x;
            this.yPos = t.localPosition.y;
            this.zPos = t.localPosition.z;
            this.thrustDir = (Quaternion.Euler(t.localEulerAngles) * t.InverseTransformDirection(t.forward));
        }

        public bool IsRoll()
        {
            if (Mathf.Abs(this.thrustDir.y) + Mathf.Abs(this.thrustDir.x) > Mathf.Abs(this.thrustDir.z))
                return true;
            return false;
        }

        public bool IsPitch()
        {
            if (Mathf.Abs(this.thrustDir.y) + Mathf.Abs(this.thrustDir.z) > Mathf.Abs(this.thrustDir.x))
                return true;
            return false;
        }

        public bool IsYaw()
        {
            if (Mathf.Abs(this.thrustDir.x) + Mathf.Abs(this.thrustDir.z) > Mathf.Abs(this.thrustDir.y))
                return true;
            return false;
        }
    }

    public enum ThrustAxis
    {
        PositiveRoll,
        NegativeRoll,
        PositiveYaw,
        NegativeYaw,
        PositivePitch,
        NegativePitch,
        Fwd,
        NoDir
    }
}
