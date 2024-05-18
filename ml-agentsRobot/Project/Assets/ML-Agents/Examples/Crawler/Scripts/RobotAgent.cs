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
    [Header("Walk Speed")]
    [Range(0.1f, m_maxWalkingSpeed)]
    private float m_TargetWalkingSpeed = m_maxWalkingSpeed;

    const float m_maxWalkingSpeed = 1; 
    public float TargetWalkingSpeed
    {
        get { return m_TargetWalkingSpeed; }
        set { m_TargetWalkingSpeed = Mathf.Clamp(value, .1f, m_maxWalkingSpeed); }
    }

    public ArticulationBody body;
    
    [Header("Body Parts")][Space(12)] 
    public Transform bodyLink;
    public Transform FRHip;
    public Transform FRLegUpper;
    public Transform FRLegLower;
    public Transform FRFoot;
    public Transform FLHip;
    public Transform FLLegUpper;
    public Transform FLLegLower;
    public Transform FLFoot;
    public Transform RRHip;
    public Transform RRLegUpper;
    public Transform RRLegLower;
    public Transform RRFoot;
    public Transform RLHip;
    public Transform RLLegUpper;
    public Transform RLLegLower;
    public Transform RLFoot;

    [Header("Target body")][Space(12)] 
    public Transform targetBody;
    public Transform targetPlayer;  
    public Transform m_OrientationCube;
    public Transform targetFRFoot;
    public Transform targetFLFoot;
    public Transform targetRRFoot;
    public Transform targetRLFoot;
    MotorController m_MoController;

    public override void Initialize()
    {
        m_MoController = GetComponent<MotorController>();

        m_MoController.SetupMotor(FRHip);
        m_MoController.SetupMotor(FRLegUpper);
        m_MoController.SetupMotor(FRLegLower);
        m_MoController.SetupMotor(FLHip);
        m_MoController.SetupMotor(FLLegUpper);
        m_MoController.SetupMotor(FLLegLower);
        m_MoController.SetupMotor(RRHip);
        m_MoController.SetupMotor(RRLegUpper);
        m_MoController.SetupMotor(RRLegLower);
        m_MoController.SetupMotor(RLHip);
        m_MoController.SetupMotor(RLLegUpper);
        m_MoController.SetupMotor(RLLegLower);

        m_MoController.SetupLink(bodyLink);
        m_MoController.SetupLink(FRFoot);
        m_MoController.SetupLink(FLFoot);
        m_MoController.SetupLink(RRFoot);
        m_MoController.SetupLink(RLFoot);
    }

    public override void OnEpisodeBegin()
    {
        foreach (var motor in m_MoController.motorsDict.Values)
        {
            motor.Reset(motor);
        }

        //body.TeleportRoot( new Vector3(0f,transform.position.y,0f), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
        body.TeleportRoot( targetBody.position + targetBody.TransformDirection(new Vector3(0f, 0.05f, 0.01f)), Quaternion.Euler(targetPlayer.rotation.eulerAngles.x,m_OrientationCube.rotation.eulerAngles.y,targetPlayer.rotation.eulerAngles.z));
        TargetWalkingSpeed = Random.Range(0.1f, m_maxWalkingSpeed);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        foreach(var angle in m_MoController.GetPosition()){
            sensor.AddObservation(angle);
        }
        sensor.AddObservation(bodyLink.rotation.eulerAngles.x);
        sensor.AddObservation(bodyLink.rotation.eulerAngles.y);
        sensor.AddObservation(bodyLink.rotation.eulerAngles.z);

        foreach (var link in m_MoController.linkDict.Keys)
        {
            if (link != bodyLink){
                sensor.AddObservation(m_MoController.linkDict[link].groundContact);
            }
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var moDict = m_MoController.motorsDict;
        var continuousActions = actionBuffers.ContinuousActions;
        var i = -1;

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
    }
    void FixedUpdate(){
        
    }

    float GetMatchingFootPosition(){
        float distance = Mathf.Abs(Vector3.Distance(new Vector3(targetFRFoot.position.x, targetFRFoot.position.y-0.02f, targetFRFoot.position.z),FRFoot.position));
        distance += Mathf.Abs(Vector3.Distance(new Vector3(targetFLFoot.position.x, targetFLFoot.position.y-0.02f, targetFLFoot.position.z),FLFoot.position));
        distance += Mathf.Abs(Vector3.Distance(new Vector3(targetRRFoot.position.x, targetRRFoot.position.y-0.02f, targetRRFoot.position.z),RRFoot.position));
        distance += Mathf.Abs(Vector3.Distance(new Vector3(targetRLFoot.position.x, targetRLFoot.position.y-0.02f, targetRLFoot.position.z),RLFoot.position)); 
        
        return -40 * distance;
    }

    float GetMatchingRootPosition(){
        float distance = Vector3.Distance(new Vector3(targetBody.position.x, targetBody.position.y-0.02f, targetBody.position.z),bodyLink.position); 
        return -20 * Mathf.Abs(distance);
    }

    float GetMatchingRootAngle(){
        float distance = Vector3.Distance(new Vector3(targetBody.position.x, targetBody.position.y-0.02f, targetBody.position.z),bodyLink.position); 
        return -10 * Mathf.Abs(distance);
    }
}
