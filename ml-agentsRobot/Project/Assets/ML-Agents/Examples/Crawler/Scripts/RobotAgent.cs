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
    float timer = 0.0f;
    float waitingTime = 20f;

    [Header("Robot Controller")]
    private MotorController m_MoController;

    [Header("Target Animation")]
    public Transform targetAnim;
    private ProceduralAnimBody proceduralAnimBody;
    private AnimController animController;


    public override void Initialize()
    {
        m_MoController = GetComponent<MotorController>();
        proceduralAnimBody = targetAnim.GetComponent<ProceduralAnimBody>();
        animController = targetAnim.GetComponent<AnimController>();
    }

    public override void OnEpisodeBegin()
    {
        animController.reset();

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

        sensor.AddObservation(animController.GetDirection().x);
        sensor.AddObservation(animController.GetDirection().y);
        sensor.AddObservation(animController.GetTargetSpeed());
        sensor.AddObservation(animController.GetTurnMode());
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var continuousActions = actionBuffers.ContinuousActions;
        var i = -1;

        m_MoController.SetMotorAngle(0,continuousActions[++i]);
        m_MoController.SetMotorAngle(1,continuousActions[++i]);
        m_MoController.SetMotorAngle(2,continuousActions[++i]);
        m_MoController.SetMotorAngle(3,continuousActions[++i]);
        m_MoController.SetMotorAngle(4,continuousActions[++i]);
        m_MoController.SetMotorAngle(5,continuousActions[++i]);
        m_MoController.SetMotorAngle(6,continuousActions[++i]);
        m_MoController.SetMotorAngle(7,continuousActions[++i]);
        m_MoController.SetMotorAngle(8,continuousActions[++i]);
        m_MoController.SetMotorAngle(9,continuousActions[++i]);
        m_MoController.SetMotorAngle(10,continuousActions[++i]);
        m_MoController.SetMotorAngle(11,continuousActions[++i]);
    }
    void FixedUpdate(){  
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            EndEpisode();
            timer = 0;
        }  
        var footReward = GetMatchingFootPosition();
        var rootReward = GetMatchingRootPosition();
        var rootAngleReward = GetMatchingRootAngle();

        AddReward(footReward + rootReward + rootAngleReward);
    }

    float GetMatchingFootPosition(){
        float distance = 0f;
        for (int i = 0; i < 4; i++){
            distance += Mathf.Abs(Vector3.Distance(proceduralAnimBody.GetFootPosition(i, new Vector3(0f,0f,0f)),m_MoController.GetFootPosition(i)));
        }
        return -40 * distance;
    }

    float GetMatchingRootPosition(){
        float distance = Vector3.Distance(proceduralAnimBody.GetRootPosition(new Vector3(0f, 0.05f, 0.01f)),m_MoController.GetRootPosition()); 
        return -20 * Mathf.Abs(distance);
    }

    float GetMatchingRootAngle(){
        float angle = Vector2.Angle(proceduralAnimBody.GetOrientationRotation(), m_MoController.GetOrientationRotation());
        return -10 * Mathf.Abs(angle);
    }
}
