using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterUtils : MonoBehaviour
{
    //The workings of this class might be slightly hard to understand fully without looking at the visualization found at: 

    /// <summary>
    /// <c>GetAxes</c> returns a list of the axes in which the thruster provides thrust.
    /// </summary>
    static public List<ThrustAxis> GetAxes(Transform t, Transform centreOfGravity)
    {
        var thr = new ThrusterTransform(t, centreOfGravity);
        var AxesList = new List<ThrustAxis>();

        RollAxis(thr);
        PitchAxis(thr);
        YawAxis(thr);
        //if (thr.IsRoll())
        //    AxesList.Add(RollAxis(thr));
        //if (thr.IsPitch())
        //    AxesList.Add(PitchAxis(thr));
        //if(thr.IsYaw())
        //    AxesList.Add(YawAxis(thr));

        return AxesList;
    }

    static private void RollAxis(ThrusterTransform thr)
    {
        var v = ExcertedForce(thr, PrincipalAxes.Roll);
        //1st quadrant
        //if (thr.xPos >= 0 && thr.yPos <= 0)
        //{
        //    if (thr.thrustDir.x <= 0 && thr.thrustDir.y <= 0)
        //        return ThrustAxis.PositiveRoll;
        //}
        ////2nd quadrant
        //if (thr.xPos <= 0 && thr.yPos <= 0)
        //{
        //    if (thr.thrustDir.x <= 0 && thr.thrustDir.y >= 0)
        //        return ThrustAxis.PositiveRoll;
        //}
        ////3rd quadrant
        //if (thr.xPos <= 0 && thr.yPos >= 0)
        //{
        //    if (thr.thrustDir.x >= 0 && thr.thrustDir.y >= 0)
        //        return ThrustAxis.PositiveRoll;
        //}
        ////4th quadrant
        //if (thr.xPos >= 0 && thr.yPos >= 0)
        //{
        //    if (thr.thrustDir.x >= 0 && thr.thrustDir.y <= 0)
        //        return ThrustAxis.PositiveRoll;
        //}
        //return ThrustAxis.NegativePitch;
    }

    static private void PitchAxis(ThrusterTransform thr)
    {
        var v = ExcertedForce(thr, PrincipalAxes.Pitch);
        //1st quadrant
        //if (thr.yPos >= 0 && thr.zPos >= 0)
        //{
        //    if (thr.thrustDir.y <= 0 && thr.thrustDir.z >= 0)
        //        return ThrustAxis.PositivePitch;
        //}
        ////2nd quadrant
        //if (thr.yPos <= 0 && thr.zPos >= 0)
        //{
        //    if (thr.thrustDir.y <= 0 && thr.thrustDir.z <= 0)
        //        return ThrustAxis.PositivePitch;
        //}
        ////3rd quadrant
        //if (thr.yPos <= 0 && thr.zPos <= 0)
        //{
        //    if (thr.thrustDir.y >= 0 && thr.thrustDir.z <= 0)
        //        return ThrustAxis.PositivePitch;
        //}
        ////4th quadrant
        //if (thr.yPos >= 0 && thr.zPos <= 0)
        //{
        //    if (thr.thrustDir.y >= 0 && thr.thrustDir.z >= 0)
        //        return ThrustAxis.PositivePitch;
        //}
        //return ThrustAxis.NegativePitch;
    }

    static private void YawAxis(ThrusterTransform thr)
    {
        var v = ExcertedForce(thr, PrincipalAxes.Yaw);
        //1st quadrant
        //if (thr.xPos >= 0 && thr.zPos >= 0)
        //{
        //    if (thr.thrustDir.x <= 0 && thr.thrustDir.z >= 0)
        //        return ThrustAxis.PositivePitch;
        //}
        ////2nd quadrant
        //if (thr.xPos <= 0 && thr.zPos >= 0)
        //{
        //    if (thr.thrustDir.x <= 0 && thr.thrustDir.z <= 0)
        //        return ThrustAxis.PositivePitch;
        //}
        ////3rd quadrant
        //if (thr.xPos <= 0 && thr.zPos <= 0)
        //{
        //    if (thr.thrustDir.x >= 0 && thr.thrustDir.z <= 0)
        //        return ThrustAxis.PositivePitch;
        //}
        ////4th quadrant
        //if (thr.xPos >= 0 && thr.zPos <= 0)
        //{
        //    if (thr.thrustDir.x >= 0 && thr.thrustDir.z >= 0)
        //        return ThrustAxis.PositivePitch;
        //}
        //return ThrustAxis.NegativePitch;
    }

    private static float ExcertedForce(ThrusterTransform thr, PrincipalAxes axis)
    {
        Vector2 pos;
        Vector2 thrustVector;
        

        var axisName = "none";
        if (axis == PrincipalAxes.Roll)
        {
            (pos, thrustVector) = thr.GetRollAxes();
            axisName = "Roll";
        }
        else if (axis == PrincipalAxes.Pitch)
        {
            (pos, thrustVector) = thr.GetPitchAxes();
            axisName = "Pitch";
        } else if (axis == PrincipalAxes.Yaw)
        {
            (pos, thrustVector) = thr.GetYawAxes();
            axisName = "Yaw";
        } else
        {
            return 0;
        }
        //? = F * r * sin(?)
        var F = new Vector2(thrustVector.x, thrustVector.y);
        var r = new Vector2(pos.x, pos.y).magnitude;
        var r_v = new Vector2(pos.x, pos.y);
        var theta = Vector2.Angle(r_v, F);
        var sinOfTheta = Mathf.Sin(theta * Mathf.Deg2Rad);
        var roundedSinOfTheta = Mathf.Round(sinOfTheta * 1000.0f) / 1000.0f;
        //var v = (F.magnitude * r) * Mathf.Sin(theta);
        var v = (F.magnitude) * roundedSinOfTheta;
        Debug.DrawLine(thr.worldPos, thr.worldPos - thr.tempWorldThrustDir, Color.red, 200);
        Debug.Log(thr.name + " yields " + axisName + " force: " + v + " angle was : " + Vector2.Angle(thrustVector, pos) + " sin of angle: " + Mathf.Round(sinOfTheta * 1000.0f) / 1000.0f);
        return v;
    }

    internal class ThrusterTransform
    {
        public float xPos;
        public float yPos;
        public float zPos;
        public Vector3 cogRelativePos;
        public Vector3 worldPos;
        //Centre of gravity
        public Vector3 cog;
        public Vector3 cogW;
        public string name;
        public Vector3 thrustDir;
        public Vector3 tempWorldThrustDir;
        public ThrusterTransform(Transform t, Transform cog)
        {
            this.xPos = t.localPosition.x;
            this.yPos = t.localPosition.y;
            this.zPos = t.localPosition.z;
            this.cogRelativePos = t.InverseTransformDirection(t.position)-t.InverseTransformDirection(cog.position);
            this.worldPos = t.position;
            this.thrustDir = (Quaternion.Euler(t.localEulerAngles) * t.InverseTransformDirection(t.forward));
            this.name = t.name;
            this.cog = cog.localPosition;
            this.cogW = cog.position;
            this.tempWorldThrustDir = t.forward;
            Debug.DrawLine(Vector3.zero, t.position, Color.magenta, 200);
            Debug.DrawLine(Vector3.zero, cog.position, Color.cyan, 200);
            Debug.DrawLine(cog.position, t.position, Color.green, 200);
            //Debug.Log(name + "- cog relative pos: " + cogRelativePos + " \n xyzPos: (" + xPos + ", " + yPos +", "+ zPos + ")");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>position vec2 and thrust vec2</returns>
        public (Vector2, Vector2) GetRollAxes()
        {
            //roll occurs around the y-axis, therefor we omit the y-axis
            Vector2 pos = new Vector2(this.cogRelativePos.x, this.cogRelativePos.z);
            Vector2 thrustVec = new Vector2(this.thrustDir.x, this.thrustDir.z);
            return (pos, thrustVec);
        }

        public (Vector2, Vector2) GetPitchAxes()
        {
            //pitch occurs around the x-axis, therefor we omit the x-axis
            Vector2 pos = new Vector2(this.cogRelativePos.y, this.cogRelativePos.z);
            Vector2 thrustVec = new Vector2(this.thrustDir.y, this.thrustDir.z);
            return (pos, thrustVec);
        }

        public (Vector2, Vector2) GetYawAxes()
        {
            //yaw occurs around the z-axis, therefor we omit the z-axis
            Vector2 pos = new Vector2(this.cogRelativePos.x, this.cogRelativePos.y);
            Vector2 thrustVec = new Vector2(this.thrustDir.x, this.thrustDir.y);
            return (pos, thrustVec);
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

    public enum PrincipalAxes
    {
        Roll,
        Pitch,
        Yaw
    }
}
