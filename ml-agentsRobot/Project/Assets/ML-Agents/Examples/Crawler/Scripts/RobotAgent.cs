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
    public Transform FRHip;
    public Transform FRlegUpper;
    public Transform FRlegLower;
    public Transform FLHip;
    public Transform FLlegUpper;
    public Transform FLlegLower;
    public Transform RRHip;
    public Transform RRlegUpper;
    public Transform RRlegLower;
    public Transform RLHip;
    public Transform RLlegUpper;
    public Transform RLlegLower;

    OrientationCubeController m_OrientationCube;
    DirectionIndicator m_DirectionIndicator;
    MotorController m_MoController;

    public override void Initialize()
    {
        m_OrientationCube = GetComponentInChildren<OrientationCubeController>();
        m_DirectionIndicator = GetComponentInChildren<DirectionIndicator>();
        m_MoController = GetComponent<MotorController>();

        m_MoController.SetupMotor(FRHip);
        m_MoController.SetupMotor(FRlegUpper);
        m_MoController.SetupMotor(FRlegLower);
        m_MoController.SetupMotor(FLHip);
        m_MoController.SetupMotor(FLlegUpper);
        m_MoController.SetupMotor(FLlegLower);
        m_MoController.SetupMotor(RRHip);
        m_MoController.SetupMotor(RRlegUpper);
        m_MoController.SetupMotor(RRlegLower);
        m_MoController.SetupMotor(RLHip);
        m_MoController.SetupMotor(RLlegUpper);
        m_MoController.SetupMotor(RLlegLower);
    }

    public override void OnEpisodeBegin()
    {
        foreach (var motor in m_MoController.motorsDict.Values)
        {
            motor.Reset();
        }

        body.TeleportRoot( new Vector3(0f,1f,0f), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));

        TargetWalkingSpeed = Random.Range(0.1f, m_maxWalkingSpeed);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var rootVel = body.velocity;
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

    }
}
