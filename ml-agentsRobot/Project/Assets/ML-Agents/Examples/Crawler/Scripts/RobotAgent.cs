using System.Collections;
using System.Collections.Generic;
using Unity.MLAgentsRobot;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgentsExamples;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class RobotAgent : Agent
{
    [Header("Robot Controller")]
    MotorController m_MoController;

    [Header("Target Animation")]
    public Transform targetAnim;
    private ProceduralAnimBody proceduralAnimBody;


    public override void Initialize()
    {
        m_MoController = GetComponent<MotorController>();
        proceduralAnimBody = targetAnim.GetComponent<ProceduralAnimBody>();

    }

    public override void OnEpisodeBegin()
    {
        foreach (var motor in m_MoController.motorsDict.Values)
        {
            motor.Reset(motor);
        }

        transform.position = new Vector3(0, 0.85f,0);

        m_MoController.RobotReset(proceduralAnimBody.GetInitPosition(new Vector3(0f, 0.05f, 0.01f)), proceduralAnimBody.GetRootRotation());
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        foreach(var angle in m_MoController.GetJointAngles()){
            sensor.AddObservation(angle);
        }
        sensor.AddObservation(m_MoController.GetRootRotation().x);
        sensor.AddObservation(m_MoController.GetRootRotation().y);
        sensor.AddObservation(m_MoController.GetRootRotation().z);

        for (int i = 0; i < 4; i++){
            sensor.AddObservation(m_MoController.GetFootContact(i));
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        //var moDict = m_MoController.motorsDict;
        var continuousActions = actionBuffers.ContinuousActions;
        var i = -1;

        for (i = 0; i < 12; i++){
            m_MoController.SetMotorAngle(i,continuousActions[i]);
        }



        /*
        moDict[FRHip].SetMotorTarget(continuousActions[++i]);
        moDict[FRLegUpper].SetMotorTarget(continuousActions[++i]);
        moDict[FRLegLower].SetMotorTarget(continuousActions[++i]);
        moDict[FLHip].SetMotorTarget(continuousActions[++i]);
        moDict[FLLegUpper].SetMotorTarget(continuousActions[++i]);
        moDict[FLLegLower].SetMotorTarget(continuousActions[++i]);
        moDict[RRHip].SetMotorTarget(continuousActions[++i]);
        moDict[RRLegUpper].SetMotorTarget(continuousActions[++i]);
        moDict[RRLegLower].SetMotorTarget(continuousActions[++i]);
        moDict[RLHip].SetMotorTarget(continuousActions[++i]);
        moDict[RLLegUpper].SetMotorTarget(continuousActions[++i]);
        moDict[RLLegLower].SetMotorTarget(continuousActions[++i]);
        */
    }
    void FixedUpdate(){  
        
        var footReward = GetMatchingFootPosition();
        var rootReward = GetMatchingRootPosition();
        var rootAngleReward = GetMatchingRootAngle();

        AddReward(footReward + rootReward + rootAngleReward);
    }

    float GetMatchingFootPosition(){
        float distance = 0f;
        for (int i = 0; i < 4; i++){
            distance += Mathf.Abs(Vector3.Distance(proceduralAnimBody.GetFootPosition(i, new Vector3(0f,-0.02f,0f)),m_MoController.GetFootPosition(i)));
        }
        //distance += Mathf.Abs(Vector3.Distance(new Vector3(targetFLFoot.position.x, targetFLFoot.position.y-0.02f, targetFLFoot.position.z),FLFoot.position));
        //distance += Mathf.Abs(Vector3.Distance(new Vector3(targetRRFoot.position.x, targetRRFoot.position.y-0.02f, targetRRFoot.position.z),RRFoot.position));
        //distance += Mathf.Abs(Vector3.Distance(new Vector3(targetRLFoot.position.x, targetRLFoot.position.y-0.02f, targetRLFoot.position.z),RLFoot.position)); 
        
        return -40 * distance;
    }

    float GetMatchingRootPosition(){
        float distance = Vector3.Distance(proceduralAnimBody.GetRootPosition(new Vector3(0f, 0.05f, 0.01f)),m_MoController.GetRootPosition()); 
        //Debug.Log(distance);
        //float distance = Vector3.Distance(new Vector3(targetBody.position.x, targetBody.position.y-0.02f, targetBody.position.z),bodyLink.position); 
        return -20 * Mathf.Abs(distance);
    }

    float GetMatchingRootAngle(){
        float angle = Vector2.Angle(proceduralAnimBody.GetOrientationRotation(), m_MoController.GetOrientationRotation());
        return -10 * Mathf.Abs(angle);
    }
}
