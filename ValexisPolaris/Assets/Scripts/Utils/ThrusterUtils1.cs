using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterUtils1 : MonoBehaviour
{
    //Debugging shit delete later :)
    static int drawings = 0;
    static int drawn = 0;

    //The workings of this class might be slightly hard to understand fully without looking at the visualization found at: 

    /// <summary>
    /// <c>GetAxes</c> returns a list of the axes in which the thruster provides thrust.
    /// </summary>
    static public List<ThrustAxis> GetAxes(Transform t, Transform centreOfGravity, Transform outerParent)
    {
        var thr = new ThrusterTransform(t, centreOfGravity, outerParent);
        var AxesList = new List<ThrustAxis>();

        AxesList.Add(RollAxis(thr));
        AxesList.Add(PitchAxis(thr));
        AxesList.Add(YawAxis(thr));
        drawn = 0;
        Debug.LogError("");
        return AxesList;
    }

    static private ThrustAxis RollAxis(ThrusterTransform thr)
    {
        var v = ExcertedForce(thr, PrincipalAxes.Roll);
        if (v > 0)
        {
            return ThrustAxis.PositiveRoll;
        }
        else if (v < 0)
        {
            return ThrustAxis.NegativeRoll;
        }
        return ThrustAxis.NoDir;
    }

    static private ThrustAxis PitchAxis(ThrusterTransform thr)
    {
        var v = ExcertedForce(thr, PrincipalAxes.Pitch);
        if (v > 0)
        {
            return ThrustAxis.PositivePitch;
        }
        else if (v < 0)
        {
            return ThrustAxis.NegativePitch;
        }
        return ThrustAxis.NoDir;
    }

    static private ThrustAxis YawAxis(ThrusterTransform thr)
    {
        var v = ExcertedForce(thr, PrincipalAxes.Yaw);
        if (v > 0)
        {
            return ThrustAxis.PositiveYaw;
        } else if (v < 0)
        {
            return ThrustAxis.NegativeYaw;
        }
        return ThrustAxis.NoDir;

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
        //? = F * r * sin(theta)
        var F = new Vector2(thrustVector.x, thrustVector.y);
        var r = new Vector2(pos.x, pos.y).magnitude;
        var r_v = new Vector2(pos.x, pos.y);
        var theta = Vector2.Angle(r_v, F);
        var sinOfTheta = Mathf.Sin(theta * Mathf.Deg2Rad);
        var roundedSinOfTheta = Mathf.Round(sinOfTheta * 1000.0f) / 1000.0f;
        var v = (F.magnitude) * roundedSinOfTheta;
        v = Mathf.Round(v * 1000.0f) / 1000.0f;

        Debug.Log(thr.name + " yields " + axisName + " force: " + v + " angle was : " + Vector2.Angle(thrustVector, pos) + " sin of angle: " + Mathf.Round(sinOfTheta * 1000.0f) / 1000.0f);
        DrawStuff(pos, thrustVector);
        drawn += 24;
        //1st quadrant
        if (pos.x >= 0 && pos.y >= 0)
        {
            if (thrustVector.x <= 0 && thrustVector.y >= 0)
                return v;
        }
        //2nd quadrant
        if (pos.x <= 0 && pos.y >= 0)
        {
            if (thrustVector.x <= 0 && thrustVector.y <= 0)
                return v;
        }
        //3rd quadrant
        if (pos.x <= 0 && pos.y <= 0)
        {
            if (thrustVector.x >= 0 && thrustVector.y <= 0)
                return v;
        }
        //4th quadrant
        if (pos.x >= 0 && pos.y <= 0)
        {
            if (thrustVector.x >= 0 && thrustVector.y >= 0)
                return v;
        }
        
        
        return -v;
    }

    private static void DrawStuff(Vector2 pos, Vector2 thrDir)
    {
        if (drawn % 72 == 0)
        {
            drawings += 24;
        }
        Vector3 ul = new Vector3(drawings-10f, 0, 10f - drawn);
        Vector3 ur = new Vector3(drawings + 10f, 0, 10f - drawn);
        Vector3 bl = new Vector3(drawings - 10f, 0, -10f - drawn);
        Vector3 br = new Vector3(drawings + 10f, 0, -10f - drawn);

        Vector3 r = new Vector3(pos.x, 0, pos.y);
        Vector3 v = new Vector3(thrDir.x, 0, thrDir.y);
        Vector3 centre = new Vector3(drawings, 1, -drawn);

        Vector3 r2 = new Vector3(centre.x + drawings + pos.x, 0, centre.z + pos.y - drawn);
        
        Vector3 v2 = new Vector3(thrDir.x+r.x, 0, thrDir.y);
        Vector3 v3 = new Vector3(thrDir.x + r.x, 0, thrDir.y + r.y);


        Debug.DrawLine(ul, ur, Color.white, 200f);
        Debug.DrawLine(bl, br, Color.white, 200f);
        Debug.DrawLine(ul, bl, Color.white, 200f);
        Debug.DrawLine(ur,br, Color.white, 200f);

        //draw push axis
        Debug.DrawLine(centre, centre + r, Color.red, 200f);
        //draw thrust vector
        Debug.DrawLine(centre, centre + v, Color.blue, 200f);



        //drawn++;




    }

    internal class ThrusterTransform
    {
        public float xPos;
        public float yPos;
        public float zPos;
        public Vector3 cogRelativePos;
        public Vector3 cogRelativePos2;
        public Vector3 worldPos;
        //Centre of gravity
        public Vector3 cog;
        public Vector3 cogW;
        public string name;
        public Vector3 thrustDir;
        public Vector3 tempWorldThrustDir;
        public ThrusterTransform(Transform t, Transform cog, Transform outerParent)
        {
            this.xPos = t.localPosition.x;
            this.yPos = t.localPosition.y;
            this.zPos = t.localPosition.z;
            //this.cogRelativePos = t.InverseTransformDirection(t.position)-cog.InverseTransformDirection(cog.position);
            this.cogRelativePos2 = t.position - cog.position;
            var v1 = outerParent.transform.InverseTransformPoint(t.position) - outerParent.transform.InverseTransformPoint(cog.position);
            this.cogRelativePos2 = t.position - cog.position;
            this.worldPos = t.position;
            this.thrustDir = (Quaternion.Euler(t.localEulerAngles) * t.InverseTransformDirection(t.forward));
            //this.thrustDir = Quaternion.Euler(t.eulerAngles)*t.forward;
            this.name = t.name;
            this.cog = cog.localPosition;
            this.cogW = cog.position;
            this.tempWorldThrustDir = outerParent.InverseTransformDirection(t.forward);
            //this.tempWorldThrustDir = outerParent.worldToLocalMatrix.MultiplyVector(t.forward); 

            Debug.DrawLine(Vector3.zero, cogW + cogRelativePos, Color.blue, 200);
            Debug.DrawLine(t.position, t.position + this.thrustDir, Color.red, 200);
            Debug.DrawLine(t.position, t.position + this.tempWorldThrustDir, Color.gray, 200);
            Debug.DrawLine(Vector3.zero, cog.position, Color.cyan, 200);
            Debug.DrawLine(cog.position, t.position, Color.green, 200);
            
            this.cogRelativePos = v1;
            Debug.Log(name + "- cog relative pos: " + cogRelativePos + " \n xyzPos: (" + xPos + ", " + yPos + ", " + zPos + ")");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>position vec2 and thrust vec2</returns>
        public (Vector2, Vector2) GetRollAxes()
        {
            //roll occurs around the y-axis, therefor we omit the y-axis
            Vector2 pos = new Vector2(this.cogRelativePos.x, this.cogRelativePos.z);
            Vector2 thrustVec = new Vector2(this.tempWorldThrustDir.x, this.tempWorldThrustDir.z);
            return (pos, thrustVec);
        }

        public (Vector2, Vector2) GetPitchAxes()
        {
            //pitch occurs around the x-axis, therefor we omit the x-axis
            Vector2 pos = new Vector2(this.cogRelativePos.z, this.cogRelativePos.y);
            Vector2 thrustVec = new Vector2(this.tempWorldThrustDir.z, this.tempWorldThrustDir.y);
            return (pos, thrustVec);
        }

        public (Vector2, Vector2) GetYawAxes()
        {
            //yaw occurs around the z-axis, therefor we omit the z-axis
            Vector2 pos = new Vector2(this.cogRelativePos.x, this.cogRelativePos.y);
            Vector2 thrustVec = new Vector2(this.tempWorldThrustDir.x, this.tempWorldThrustDir.y);
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
        NoDir
    }

    public enum PrincipalAxes
    {
        Roll,
        Pitch,
        Yaw
    }
}
