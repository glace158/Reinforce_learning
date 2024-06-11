using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointMaching : MonoBehaviour
{
    [Header("Anime Joint")]
    public Transform animHip;
    public Transform animUpper;
    public Transform animLower;
    public Vector3[] offsetAngles = new Vector3[3]; 

    [Header("Robot Joint")]
    public ArticulationBody robotHip;
    public ArticulationBody robotUpper;
    public ArticulationBody robotLower;
    // Start is called before the first frame update
    void Start()
    {
        offsetAngles[0] = UnityEditor.TransformUtils.GetInspectorRotation(animHip);
        offsetAngles[1] = UnityEditor.TransformUtils.GetInspectorRotation(animUpper);
        offsetAngles[2] = UnityEditor.TransformUtils.GetInspectorRotation(animLower);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(GetAnimJointAngle(animHip, Vector3.left, offsetAngles[0]));
        SetJointAngle(robotHip, GetAnimJointAngle(animHip, Vector3.left, offsetAngles[0]));

        Debug.Log(GetAnimJointAngle(animUpper, Vector3.back, offsetAngles[1]));
        SetJointAngle(robotUpper, GetAnimJointAngle(animUpper, Vector3.back, offsetAngles[1]));

        Debug.Log(GetAnimJointAngle(animLower, Vector3.left, offsetAngles[2]));
        SetJointAngle(robotLower, GetAnimJointAngle(animLower, Vector3.left, offsetAngles[2]));
    }

    float GetAnimJointAngle(Transform t, Vector3 axis, Vector3 offset){
        Vector3 axisAngle = (Vector3.Scale(UnityEditor.TransformUtils.GetInspectorRotation(t), axis) - Vector3.Scale(offset, axis));
        if(Mathf.Abs(axis.x) == 1){//x
            return axisAngle.x;
        }
        else if(Mathf.Abs(axis.y) == 1){//y
            return axisAngle.y;
        }
        else if(Mathf.Abs(axis.z) == 1){//z
            return axisAngle.z;
        }
        else{
            return -1f;
        }
        
    }

    public void SetJointAngle(ArticulationBody motor, float angle){
            var mo_drive = motor.xDrive;

            mo_drive.target = angle;

            motor.xDrive = mo_drive;
        }
}
