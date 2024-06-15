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


    float previousMotorRotation;
    float previsousAngle;

    void Start()
    {
        offsetAngles[0] = UnityEditor.TransformUtils.GetInspectorRotation(animHip);
        offsetAngles[1] = UnityEditor.TransformUtils.GetInspectorRotation(animUpper);
        offsetAngles[2] = UnityEditor.TransformUtils.GetInspectorRotation(animLower);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //SetJointAngle(robotHip, GetAnimJointAngle(animHip, Vector3.right, offsetAngles[0]));

        //SetJointAngle(robotUpper, GetAnimJointAngle(animUpper, Vector3.forward, offsetAngles[1]));

        //SetJointAngle(robotLower, GetAnimJointAngle(animLower, Vector3.left, offsetAngles[2]));
        var angleError = GetAnimJointAngle(animLower, Vector3.left, offsetAngles[2]) - robotLower.jointPosition[0] * 180 / Mathf.PI;
        //var angularError = GetMotorAngularVelocity(robotLower) - GetAngularVelocity(animLower, Vector3.left);
        Debug.Log(Mathf.Exp(-0.01f * Mathf.Pow(angleError,2)));
    }

    float GetAnimJointAngle(Transform t, Vector3 axis, Vector3 offset){
        Vector3 axisAngle = (Vector3.Scale(UnityEditor.TransformUtils.GetInspectorRotation(t), axis) - Vector3.Scale(offset, axis));
        //Vector3 axisAngle =UnityEditor.TransformUtils.GetInspectorRotation(t) - offset;
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

    public float GetMotorAngularVelocity(ArticulationBody motor){
        var x = motor.jointPosition[0] * 180 / Mathf.PI;
        float deltaRotation = x - previousMotorRotation;
        previousMotorRotation = x;

		//각도에서 라디안으로 변환
        deltaRotation *= Mathf.Deg2Rad;

        var motorAngularVelocity = (1.0f / Time.deltaTime) * deltaRotation;
        return Mathf.Abs(motorAngularVelocity);        
    }

    public float GetAngularVelocity(Transform t, Vector3 axis)
    {
        var axisAngle = UnityEditor.TransformUtils.GetInspectorRotation(t);
        var angle = -1f;
        if(Mathf.Abs(axis.x) == 1){//x
            angle = axisAngle.x;
        }
        else if(Mathf.Abs(axis.y) == 1){//y
            angle = axisAngle.y;
        }
        else if(Mathf.Abs(axis.z) == 1){//z
            angle = axisAngle.z;
        }

        float deltaRotation = angle - previsousAngle;
        previsousAngle = angle;

        
		//각도에서 라디안으로 변환
        deltaRotation *= Mathf.Deg2Rad;

        float Velocity = (1.0f / Time.deltaTime) * deltaRotation;

		//각속도 반환
        return Mathf.Abs(Velocity);
    }
}
