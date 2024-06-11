using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAngleMatch : MonoBehaviour
{
    [Header("Anim Joint")]
    public Transform animHip;
    public Transform animUpper;
    public Transform animLower;
    [Header("Robot Joint")]
    public ArticulationBody robotHip;
    public ArticulationBody robotUpper;
    public ArticulationBody robotLower;

    public bool is_offset = true;
    float[] offset = new float[3];
    // Start is called before the first frame update
    void Start()
    {
        initOffset();
    }   

    void initOffset(){
        offset[0] = UnityEditor.TransformUtils.GetInspectorRotation(animHip).x;
        offset[1] = UnityEditor.TransformUtils.GetInspectorRotation(animUpper).y;
        offset[2] = UnityEditor.TransformUtils.GetInspectorRotation(animLower).x;
        Debug.Log("offset: " + offset[0]);
        Debug.Log("offset: " + offset[1]);
        Debug.Log("offset: " + offset[2]);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Hip: " + GetAnimAngle(animHip, 0,offset[0], 1));
        Debug.Log("Upper: " + GetAnimAngle(animUpper, 1,offset[1], 1));
        Debug.Log("Lower: " + GetAnimAngle(animLower, 0,offset[2], 1));

        if(is_offset){
            initOffset();
            is_offset = false;
        }

        SetRobotAngle(robotHip, GetAnimAngle(animHip, 0,offset[0], -1));
        SetRobotAngle(robotUpper, GetAnimAngle(animUpper, 1,offset[1], -1));
        SetRobotAngle(robotLower, GetAnimAngle(animLower, 0,offset[2], -1));
    }

    float GetAnimAngle(Transform t, int axis, float offset,int val){
        switch (axis){
            case 0:
                return (UnityEditor.TransformUtils.GetInspectorRotation(t).x - offset) * val;
            case 1:
                return (UnityEditor.TransformUtils.GetInspectorRotation(t).y - offset) * val;
            case 2:
                return (UnityEditor.TransformUtils.GetInspectorRotation(t).z - offset) * val;
            default:
                return 0f;
        }
    }

    void SetRobotAngle(ArticulationBody motor, float angle){
        var mo_drive = motor.xDrive;

        mo_drive.target = angle;

        motor.xDrive = mo_drive;
    }
}
