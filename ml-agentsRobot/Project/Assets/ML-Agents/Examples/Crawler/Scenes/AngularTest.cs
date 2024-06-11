using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularTest : MonoBehaviour
{
    public Transform AnimJoint;
    public Transform AnimTarget;
    public Transform RobotJoint; 
    public Transform RobotFoot;

    public ArticulationBody motor;

    Quaternion previousRotation; //전 프레임의 로테이션 값
    Vector3 angularVelocity; //각속도를 관리할 변수
    
    float previousMotorRotation;
    float previsousAngle;
    
    Rigidbody rd;

    void Start(){
        previousRotation =AnimJoint.rotation;
        rd = AnimJoint.GetComponent<Rigidbody>();
        motor =  RobotJoint.GetComponent<ArticulationBody>();

        previousMotorRotation = UnityEditor.TransformUtils.GetInspectorRotation(RobotJoint.transform).x;
    
        previsousAngle = UnityEditor.TransformUtils.GetInspectorRotation(AnimJoint.transform).x;
    }
    // Update is called once per frame
    void Update()
    {
        AnimTarget.position = RobotFoot.position;
        Debug.Log("---------------------------------------");
        var animVel = GetAngularVelocity();
        var motorVel = GetMotorAngularVelocity();
        Debug.Log("AnimeRD: " + animVel + " | Robot: " + motorVel);
        
        var angle = Mathf.Pow( Mathf.Abs(animVel) - Mathf.Abs(motorVel), 2);
        Debug.Log( Mathf.Exp(-0.1f * angle) );
    }

    public float GetMotorAngularVelocity(){
        var x = motor.jointPosition[0] * 180 / Mathf.PI;
        float deltaRotation = x - previousMotorRotation;
        previousMotorRotation = x;
        //Debug.Log(motor.jointPosition[0] * 180 / Mathf.PI);

        
		//각도에서 라디안으로 변환
        deltaRotation *= Mathf.Deg2Rad;

        var motorAngularVelocity = (1.0f / Time.deltaTime) * deltaRotation;
        return motorAngularVelocity;        
    }

    public float GetAngularVelocity()
    {
        //var x = UnityEditor.TransformUtils.GetInspectorRotation(AnimJoint.transform).x;
        var x = AnimJoint.localEulerAngles.y;
        Debug.Log(x);
        //x = (x + 155.023f) * (25f + 71f) / (-65.374f - (-155.023f)) -71f;
        float deltaRotation = x - previsousAngle;
        previsousAngle = x;

        
		//각도에서 라디안으로 변환
        deltaRotation *= Mathf.Deg2Rad;

        float Velocity = (1.0f / Time.deltaTime) * deltaRotation;

		//각속도 반환
        return Velocity;
    }

    public float remap(float x, float in_min, float in_max, float out_min, float out_max){
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
