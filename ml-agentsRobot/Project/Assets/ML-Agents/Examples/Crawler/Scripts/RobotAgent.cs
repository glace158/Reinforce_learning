using System.Collections;
using System.Collections.Generic;
using Unity.MLAgentsRobot;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgentsExamples;
using Unity.MLAgents.Sensors;
using UnityEngine;
using System.Linq;
using System;

public class RobotAgent : Agent
{
    float timer = 0.0f;
    float waitingTime = 20f;

    [Header("Robot Controller")]
    private MotorController m_MoController;

    [Header("Target Animation")]
    public GameObject AnimCharator;
    public Transform targetAnim;
    public Transform lookTargetCube;
    private ProceduralAnimBody proceduralAnimBody;
    private AnimController animController;
    private Vector3 initAnimPos;

    private bool animReset = false;

    private float[] previousActions = new float[12] {0f,0f,0f,0f,0f,0f,0f,0f,0f,0f,0f,0f};

    EnvironmentParameters m_ResetParams;
        
    public override void Initialize()
    {
        m_MoController = GetComponent<MotorController>();
        proceduralAnimBody = targetAnim.GetComponent<ProceduralAnimBody>();
        animController = targetAnim.GetComponent<AnimController>();
        initAnimPos = targetAnim.position;

        m_ResetParams = Academy.Instance.EnvironmentParameters;
    }

    public override void OnEpisodeBegin()
    {
        Physics.IgnoreLayerCollision(2, 2, true);
        if (animReset){
            Destroy(targetAnim.gameObject);
            targetAnim = Instantiate(AnimCharator, initAnimPos, Quaternion.Euler(0,0,0)).transform;
            targetAnim.SetParent(this.transform.parent);
            
            proceduralAnimBody = targetAnim.GetComponent<ProceduralAnimBody>();
            animController = targetAnim.GetComponent<AnimController>();
            
            animController.SetLookTarget(lookTargetCube);
            proceduralAnimBody.SetOrientationCube(lookTargetCube);

            m_MoController.RobotReset(proceduralAnimBody.GetInitPosition(new Vector3(0f, 0.00f, 0.05f)), Quaternion.Euler(proceduralAnimBody.GetRootRotation()));
            animReset = false;
            ConfigureAgent();
        }
        else{
            SetMatchingAngle();
        }
        
        Physics.IgnoreLayerCollision(2, 2, false);
    }

    void ConfigureAgent(){
        animController.SetMaxSpeed(m_ResetParams.GetWithDefault("max_Speed", 1f));
    }

    public void SetMatchingAngle(){
        try{
            m_MoController.RobotReset(proceduralAnimBody.GetInitPosition(new Vector3(0f, 0.00f, 0.05f)), Quaternion.Euler(proceduralAnimBody.GetRootRotation()));
                
            for (int i = 0; i < 12; i++){
                var targetAngle = proceduralAnimBody.GetJointAngle(i);
                m_MoController.SetJointAngle(i, targetAngle);
            }
        }
        catch(Exception e)
        { 
            var temp = e;
        } 
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        for (int i = 0; i < 12;i++){
            sensor.AddObservation(m_MoController.GetMotorAngles(i));
        }

        sensor.AddObservation(m_MoController.GetRootRotation().x);//1
        sensor.AddObservation(m_MoController.GetRootRotation().y);//1
        sensor.AddObservation(m_MoController.GetRootRotation().z);//1

        for (int i = 0; i < 4; i++){
            sensor.AddObservation(m_MoController.GetFootContact(i));//4
        }

        sensor.AddObservation(animController.GetDirection().x);//1
        sensor.AddObservation(animController.GetDirection().y);//1
        sensor.AddObservation(animController.GetTargetSpeed());//1
        sensor.AddObservation(animController.GetTurnMode());//1

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var continuousActions = actionBuffers.ContinuousActions;
        var i = -1;
        var bata = 0.2f;

        m_MoController.SetMotorAngle(0,LowPassFilter(bata, continuousActions[++i], previousActions[0]));
        previousActions[i] = continuousActions[i];
        m_MoController.SetMotorAngle(1,LowPassFilter(bata, continuousActions[++i], previousActions[1]));
        previousActions[i] = continuousActions[i];
        m_MoController.SetMotorAngle(2,LowPassFilter(bata, continuousActions[++i], previousActions[2]));
        previousActions[i] = continuousActions[i];
        m_MoController.SetMotorAngle(3,LowPassFilter(bata, continuousActions[++i], previousActions[3]));
        previousActions[i] = continuousActions[i];
        m_MoController.SetMotorAngle(4,LowPassFilter(bata, continuousActions[++i], previousActions[4]));
        previousActions[i] = continuousActions[i];
        m_MoController.SetMotorAngle(5,LowPassFilter(bata, continuousActions[++i], previousActions[5]));
        previousActions[i] = continuousActions[i];
        m_MoController.SetMotorAngle(6,LowPassFilter(bata, continuousActions[++i], previousActions[6]));
        previousActions[i] = continuousActions[i];
        m_MoController.SetMotorAngle(7,LowPassFilter(bata, continuousActions[++i], previousActions[7]));
        previousActions[i] = continuousActions[i];
        m_MoController.SetMotorAngle(8,LowPassFilter(bata, continuousActions[++i], previousActions[8]));
        previousActions[i] = continuousActions[i];
        m_MoController.SetMotorAngle(9,LowPassFilter(bata, continuousActions[++i], previousActions[9]));
        previousActions[i] = continuousActions[i];
        m_MoController.SetMotorAngle(10,LowPassFilter(bata, continuousActions[++i], previousActions[10]));
        previousActions[i] = continuousActions[i];
        m_MoController.SetMotorAngle(11,LowPassFilter(bata, continuousActions[++i], previousActions[11]));
        previousActions[i] = continuousActions[i];

        var footReward = GetMatchingFootPosition();
        var rootReward = GetMatchingRootPosition();
        var rootAngleReward = GetMatchingRootAngle();
        var jointReward = GetJointAngleCompare();
        var jointVelocityReward = GetJointVelocityCompare();
        var rootSpeedReward = GetMatchingRootSpeed();

        //var actionPenalty = GetActionPenalty();
        
        AddReward(footReward);
        AddReward(rootReward);
        AddReward(rootAngleReward);
        AddReward(jointReward);
        AddReward(jointVelocityReward);
        AddReward(rootSpeedReward);
        //AddReward(actionPenalty);
    }
    void FixedUpdate(){  
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            animReset = true;
            EndEpisode();
            timer = 0;
        } 

        
        var positionMagnitude = Mathf.Clamp(Vector3.Distance(proceduralAnimBody.GetRootPosition(new Vector3(0f, 0f, -0.05f)),m_MoController.GetRootPosition()), 0f, 10f);
        float distance = Mathf.Pow(1 - Mathf.Pow(positionMagnitude / 2f, 2), 2);
        if (distance < 0.01){
            SetReward(-1f);
            EndEpisode();
        }
    }


    float LowPassFilter(float bata, float currentAction, float previousAction){
        return bata * currentAction + (1f - bata) * previousAction;
    }

    float GetMatchingFootPosition(){
        float distance = 0f;
        for (int i = 0; i < 4; i++){
            distance += Vector3.Distance(proceduralAnimBody.GetFootPosition(i, new Vector3(0f,0f,0f)),m_MoController.GetFootPosition(i));
        }
        return Mathf.Exp(-40 * distance);
    }

    float GetMatchingRootPosition(){
        float distance = Vector3.Distance(proceduralAnimBody.GetRootPosition(new Vector3(0f, 0f, -0.05f)),m_MoController.GetRootPosition()); 
        
        return Mathf.Exp(-40 * distance);
    }

    float GetMatchingRootAngle(){
        float angle = Vector2.Angle(proceduralAnimBody.GetOrientationRotation(), m_MoController.GetOrientationRotation());
        angle = Mathf.Exp(-0.01f * Mathf.Pow(angle,2));
        return angle;
    }

    float GetMatchingRootSpeed(){
        return Mathf.Exp( -30 * Mathf.Pow(proceduralAnimBody.GetRootSpeed() - m_MoController.GetRootSpeed(), 2));
    }
    /*
    float GetActionPenalty(){
        float penalty = 0f;
        for (int i = 0; i < currentActions.Length; i++){
            //penalty += Mathf.Pow(currentActions[i] - m_MoController.GetMotorAngles(i), 2);
            penalty += Mathf.Pow(currentActions[i], 2);
        }
        return Mathf.Exp(-0.01f * penalty);
    }
    */
    float GetJointAngleCompare(){
        try{
            var angle = 0f;
            for (int i = 0; i < 12; i++){
                angle += Mathf.Pow(m_MoController.GetJointAngles(i) - proceduralAnimBody.GetJointAngle(i),2);
            }
            return Mathf.Exp(-0.01f * angle);
        }
        catch(Exception e)
        { 
            var temp = e;
            return 0f; 
        } 
    }

    float GetJointVelocityCompare(){
        try{
            var velocity = 0f;
            for(int i = 0;i < 12; i++){
                velocity += Mathf.Pow(m_MoController.GetJointVelocity(i) - proceduralAnimBody.GetJointVelocity(i),2);
            }
            return Mathf.Exp(-0.1f * velocity);
        }
        catch (Exception e){
            var temp = e;
            return 0;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut){

    }
}
